CREATE OR REPLACE FUNCTION public.mt_immutable_timestamp(value text) RETURNS timestamp without time zone LANGUAGE sql IMMUTABLE AS $function$
    select value::timestamp
$function$;

CREATE OR REPLACE FUNCTION public.mt_immutable_timestamptz(value text) RETURNS timestamp with time zone LANGUAGE sql IMMUTABLE AS $function$
    select value::timestamptz
$function$;

DROP TABLE IF EXISTS public.mt_doc_rs7 CASCADE;
CREATE TABLE public.mt_doc_rs7 (
    id                  integer CONSTRAINT pk_mt_doc_rs7 PRIMARY KEY,
    data                jsonb NOT NULL,
    mt_last_modified    timestamp with time zone DEFAULT transaction_timestamp(),
    mt_version          uuid NOT NULL default(md5(random()::text || clock_timestamp()::text)::uuid),
    mt_dotnet_type      varchar 
);
COMMENT ON TABLE public.mt_doc_rs7 IS 'origin:Marten.IDocumentStore, Marten, Version=3.13.2.0, Culture=neutral, PublicKeyToken=null';

CREATE OR REPLACE FUNCTION public.mt_upsert_rs7(doc JSONB, docDotNetType varchar, docId integer, docVersion uuid) RETURNS UUID LANGUAGE plpgsql SECURITY INVOKER AS $function$
DECLARE
  final_version uuid;
BEGIN
INSERT INTO public.mt_doc_rs7 ("data", "mt_dotnet_type", "id", "mt_version", mt_last_modified) VALUES (doc, docDotNetType, docId, docVersion, transaction_timestamp())
  ON CONFLICT ON CONSTRAINT pk_mt_doc_rs7
  DO UPDATE SET "data" = doc, "mt_dotnet_type" = docDotNetType, "mt_version" = docVersion, mt_last_modified = transaction_timestamp();

  SELECT mt_version FROM public.mt_doc_rs7 into final_version WHERE id = docId ;
  RETURN final_version;
END;
$function$;


CREATE OR REPLACE FUNCTION public.mt_insert_rs7(doc JSONB, docDotNetType varchar, docId integer, docVersion uuid) RETURNS UUID LANGUAGE plpgsql SECURITY INVOKER AS $function$
BEGIN
INSERT INTO public.mt_doc_rs7 ("data", "mt_dotnet_type", "id", "mt_version", mt_last_modified) VALUES (doc, docDotNetType, docId, docVersion, transaction_timestamp());

  RETURN docVersion;
END;
$function$;


CREATE OR REPLACE FUNCTION public.mt_update_rs7(doc JSONB, docDotNetType varchar, docId integer, docVersion uuid) RETURNS UUID LANGUAGE plpgsql SECURITY INVOKER AS $function$
DECLARE
  final_version uuid;
BEGIN
  UPDATE public.mt_doc_rs7 SET "data" = doc, "mt_dotnet_type" = docDotNetType, "mt_version" = docVersion, mt_last_modified = transaction_timestamp() where id = docId;

  SELECT mt_version FROM public.mt_doc_rs7 into final_version WHERE id = docId ;
  RETURN final_version;
END;
$function$;

DROP TABLE IF EXISTS public.mt_hilo CASCADE;
CREATE TABLE public.mt_hilo (
    entity_name    varchar CONSTRAINT pk_mt_hilo PRIMARY KEY,
    hi_value       bigint default 0
);
COMMENT ON TABLE public.mt_hilo IS 'origin:Marten.IDocumentStore, Marten, Version=3.13.2.0, Culture=neutral, PublicKeyToken=null';
CREATE OR REPLACE FUNCTION public.mt_get_next_hi(entity varchar) RETURNS integer AS $$
DECLARE
	current_value bigint;
	next_value bigint;
BEGIN
	select hi_value into current_value from public.mt_hilo where entity_name = entity;
	IF current_value is null THEN
		insert into public.mt_hilo (entity_name, hi_value) values (entity, 0);
		next_value := 0;
	ELSE
		next_value := current_value + 1;
		update public.mt_hilo set hi_value = next_value where entity_name = entity and hi_value = current_value;

        IF NOT FOUND THEN
            next_value := -1;
        END IF;

	END IF;

	return next_value;
END
$$ LANGUAGE plpgsql;



