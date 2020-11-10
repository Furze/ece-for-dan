drop function if exists public.mt_immutable_timestamp(text) cascade;
drop function if exists public.mt_immutable_timestamptz(text) cascade;
-- Marten.Storage.DocumentTable
drop table if exists public.mt_doc_rs7 cascade;


drop function if exists public.mt_upsert_rs7(JSONB, varchar, integer, uuid);
drop function if exists public.mt_insert_rs7(JSONB, varchar, integer, uuid);
drop function if exists public.mt_update_rs7(JSONB, varchar, integer, uuid);
-- Marten.Storage.Table
drop table if exists public.mt_hilo cascade;


drop function if exists public.mt_get_next_hi(varchar) cascade;

