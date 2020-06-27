CREATE SCHEMA cv19mon
    AUTHORIZATION root;
	
CREATE COLLATION cv19mon."pt_BR.utf8"
    (LC_COLLATE = 'pt-BR', LC_CTYPE = 'pt-BR');

ALTER COLLATION cv19mon."pt_BR.utf8"
    OWNER TO root;
	
	
	
CREATE TABLE IF NOT EXISTS cv19mon.ibge
(
    ibge_code integer NOT NULL,
    city character varying(255) COLLATE cv19mon."pt_BR.utf8" NOT NULL,
    region_code character(2) COLLATE cv19mon."pt_BR.utf8" NOT NULL,
    CONSTRAINT pk_cases PRIMARY KEY (ibge_code)
)

TABLESPACE pg_default;

ALTER TABLE cv19mon.ibge
    OWNER to root;
	
GRANT ALL ON TABLE cv19mon.ibge TO root;



CREATE TABLE IF NOT EXISTS cv19mon.cases
(
    ibge_code integer,
    entry_date date NOT NULL,
    publishing_index integer NOT NULL,
    current_confirmed integer NOT NULL,
    new_confirmed integer NOT NULL,
    new_deaths integer NOT NULL,
    current_deaths integer NOT NULL,
    death_rate real NOT NULL,
    CONSTRAINT fk_cases_ibge_ibge_code FOREIGN KEY (ibge_code)
        REFERENCES cv19mon.ibge (ibge_code) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE cv19mon.cases
    OWNER to root;

GRANT ALL ON TABLE cv19mon.cases TO root;



CREATE USER u_covid19 
	WITH ENCRYPTED PASSWORD '@C0v1dl9';

GRANT SELECT ON TABLE cv19mon.ibge TO u_covid19;	
	
GRANT SELECT ON TABLE cv19mon.cases TO u_covid19;	