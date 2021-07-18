CREATE TABLE IF NOT EXISTS users (
    id serial primary key ,
    first_name varchar(128) not null ,
    last_name varchar(128) not null
);
