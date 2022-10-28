CREATE DATABASE supers
    WITH
    OWNER = postgres
    ENCODING = 'UTF8'
    IS_TEMPLATE = False;

COMMENT ON DATABASE supers
    IS 'Database for The guardians of the globe';

CREATE TABLE public.super
(
    super_id character varying NOT NULL,
    nombre character varying NOT NULL,
    Edad integer NOT NULL,
    rol_super character varying NOT NULL,
    relaciones character varying,
    origen character varying,
    PRIMARY KEY (super_id)
    ADD CONSTRAINT check_rol CHECK (rol_super::text = 'Heroe'::text OR rol_super::text = 'Villano'::text);
);

ALTER TABLE IF EXISTS public.super
    OWNER to postgres;

COMMENT ON TABLE public.super
    IS 'Basic information of Heroes and Villains';

CREATE TABLE IF NOT EXISTS public.rasgo
(
    rasgo_id character varying COLLATE pg_catalog."default" NOT NULL,
    titulo character varying COLLATE pg_catalog."default" NOT NULL,
    tipo_rasgo character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT rasgo_pkey PRIMARY KEY (rasgo_id),
    CONSTRAINT rasgo_titulo_key UNIQUE (titulo),
    CONSTRAINT check_tipo_rasgo CHECK (tipo_rasgo::text = 'Habilidad'::text OR tipo_rasgo::text = 'Debilidad'::text)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.rasgo
    OWNER to postgres;

COMMENT ON TABLE public.rasgo
    IS 'Rasgos de super';

CREATE TABLE IF NOT EXISTS public.rasgo_super
(
    rasgo_super_id character varying COLLATE pg_catalog."default" NOT NULL,
    rasgo_id character varying COLLATE pg_catalog."default" NOT NULL,
    super_id character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT rasgo_super_pkey PRIMARY KEY (rasgo_super_id),
    CONSTRAINT rasgo_super_rasgo_id_fkey FOREIGN KEY (rasgo_id)
        REFERENCES public.rasgo (rasgo_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT rasgo_super_super_id_fkey FOREIGN KEY (super_id)
        REFERENCES public.super (super_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.rasgo_super
    OWNER to postgres;

COMMENT ON TABLE public.rasgo_super
    IS 'Rasgos para supers';

CREATE TABLE public.enfrentamiento
(
    enfrentamiento_id character varying NOT NULL,
    titulo character varying NOT NULL,
    fecha date NOT NULL,
    PRIMARY KEY (enfrentamiento_id)
);

ALTER TABLE IF EXISTS public.enfrentamiento
    OWNER to postgres;

CREATE TABLE public.pelea
(
    pelea_id character varying NOT NULL,
    super_id character varying NOT NULL,
    enfrentamiento_id character varying NOT NULL,
    gana boolean NOT NULL,
    PRIMARY KEY (pelea_id),
    FOREIGN KEY (super_id)
        REFERENCES public.super (super_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
        NOT VALID,
    FOREIGN KEY (enfrentamiento_id)
        REFERENCES public.enfrentamiento (enfrentamiento_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
        NOT VALID
);

ALTER TABLE IF EXISTS public.pelea
    OWNER to postgres;

CREATE TABLE public.evento
(
    evento_id character varying NOT NULL,
    super_id character varying NOT NULL,
    titulo character varying NOT NULL,
    inicio date NOT NULL,
    fin date NOT NULL,
    descripcion character varying,
    lugar character varying,
    PRIMARY KEY (evento_id),
    FOREIGN KEY (super_id)
        REFERENCES public.super (super_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
        NOT VALID
);

ALTER TABLE IF EXISTS public.evento
    OWNER to postgres;

CREATE TABLE IF NOT EXISTS public.patrocinador
(
    patrocinador_id character varying COLLATE pg_catalog."default" NOT NULL,
    super_id character varying COLLATE pg_catalog."default" NOT NULL,
    nombre character(1) COLLATE pg_catalog."default" NOT NULL,
    monto numeric NOT NULL,
    origen_monto character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT patrocinador_pkey PRIMARY KEY (patrocinador_id),
    CONSTRAINT patrocinador_super_id_fkey FOREIGN KEY (super_id)
        REFERENCES public.super (super_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.patrocinador
    OWNER to postgres;