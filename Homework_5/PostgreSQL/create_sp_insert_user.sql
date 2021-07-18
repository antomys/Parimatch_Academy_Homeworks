CREATE OR REPLACE PROCEDURE add_user(_first_name varchar, _last_name varchar)
LANGUAGE plpgsql AS $$
begin
    if not exists (select 1 from users where _first_name = first_name and _last_name = last_name) then
        Insert Into volokhovych_notes.public.users(first_name, last_name) values (_first_name,_last_name);
    end if;
end;
    $$