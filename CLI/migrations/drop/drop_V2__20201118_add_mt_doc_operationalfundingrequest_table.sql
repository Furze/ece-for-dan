-- Marten.Storage.DocumentTable
drop table if exists public.mt_doc_operationalfundingrequest cascade;


drop function if exists public.mt_upsert_operationalfundingrequest(JSONB, varchar, integer, uuid);
drop function if exists public.mt_insert_operationalfundingrequest(JSONB, varchar, integer, uuid);
drop function if exists public.mt_update_operationalfundingrequest(JSONB, varchar, integer, uuid);

