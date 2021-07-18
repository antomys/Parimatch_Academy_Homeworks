DO
$do$
BEGIN
   IF EXISTS (SELECT FROM pg_database WHERE datname = 'volokhovych_notes') THEN
      RAISE NOTICE 'Database already exists';  -- optional
   ELSE
      PERFORM dblink_exec('dbname=' || current_database()  -- current db
                        , 'CREATE DATABASE volokhovych_notes');
   END IF;
END
$do$;