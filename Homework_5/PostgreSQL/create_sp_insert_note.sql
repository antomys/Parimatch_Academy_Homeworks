CREATE OR REPLACE PROCEDURE add_note
    (_note_id uuid,
    _header varchar,
    _body varchar,
    _user_id int)
LANGUAGE plpgsql
AS $$
begin
    if not exists(select 1 from notes where header = _header AND
                                        body= _body) THEN
                                            if exists(select id from volokhovych_notes.public.users where id = _user_id) then
                                                Insert Into notes(id, header, body, is_deleted, user_id, modified_at)
                                                values (_note_id,_header,_body,false,_user_id,current_timestamp);
                                            end if;

    end if;
end;
$$