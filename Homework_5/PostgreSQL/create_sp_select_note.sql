CREATE OR REPLACE function select_note( note_id uuid)
RETURNS TABLE (
    _id uuid,
    _header varchar,
    _body varchar,
    _is_deleted boolean,
    _user_id int,
    _modified_at timestamp with time zone,
    _first_name varchar,
    _last_name varchar
    )
language plpgsql as $$
begin
    if exists(select id from notes where id = note_id) then
        return query (select notes.id,header,body,is_deleted,notes.user_id,modified_at,first_name,last_name from notes inner join users u on u.id = notes.user_id where notes.id = note_id);
    end if;
end;
$$