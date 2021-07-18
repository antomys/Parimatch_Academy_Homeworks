create or replace function select_user_notes(id_of_user int)
returns table(
_id uuid,
_header varchar,
_body varchar,
_is_deleted boolean,
_user_id int,
_modified_at timestamp with time zone
             )
             language plpgsql
as
$$
    begin
        if exists(select user_id from notes where user_id = id_of_user) then
            return query
            select * from notes
            where user_id = id_of_user and is_deleted = false;
        end if;
    end;
$$