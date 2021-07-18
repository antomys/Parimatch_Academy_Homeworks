create or replace function users_notes_count()
returns table(
    _id integer,
    _first_name varchar,
    _last_nave varchar,
    _not_deleted_amount bigint
             )
LANGUAGE plpgsql AS $$
begin
    return query(
        select
               users.id,
               first_name,
               last_name,
               count(nullif(is_deleted,true)) as not_deleted_amount
        from volokhovych_notes.public.users inner join notes n on users.id = n.user_id
    group by users.id);
end;
$$