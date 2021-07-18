create or replace function note_mark_deleted(note_id uuid)
returns varchar as $$
    begin
        if exists(select id from notes where id = note_id) then
            if(select is_deleted from notes where id = note_id and is_deleted is true) then
                return 'false - flag is_deleted is already true!';
            end if;
            UPDATE notes set is_deleted = true where id = note_id;
            return 'true - this UUID exists and flag is changed!';
        end if;
        return 'false - this UUID does not exist!';
    end;
    $$ LANGUAGE plpgsql;