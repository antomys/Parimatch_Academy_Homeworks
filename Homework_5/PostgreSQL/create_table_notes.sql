CREATE TABLE if not exists notes(
    id uuid primary key ,
    header varchar(128) not null ,
    body varchar(1024) not null ,
    is_deleted boolean not null ,
    user_id serial references volokhovych_notes.public.users not null ,
    modified_at timestamp with time zone default current_timestamp not null
);
CREATE INDEX if not exists idx_modified_ad on notes(modified_at);