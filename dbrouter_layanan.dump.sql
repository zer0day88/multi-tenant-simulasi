--
-- PostgreSQL database dump
--

-- Dumped from database version 13.3 (Debian 13.3-1.pgdg100+1)
-- Dumped by pg_dump version 13.3 (Debian 13.3-1.pgdg100+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: auth_check(character varying, bigint); Type: FUNCTION; Schema: public; Owner: admin
--

CREATE FUNCTION public.auth_check(_email character varying, _id_tenant bigint) RETURNS TABLE(id_user bigint, email character varying, password character varying, id_tenant bigint)
    LANGUAGE sql
    AS $$
select ua.id_user,
       ua.email,
       ua.password,
       lt.id_tenant
from user_account ua
inner join user_tenant ut on ua.id_user = ut.id_user
inner join layanan_tenant lt on ut.id_tenant = lt.id_tenant
where email = _email and lt.id_tenant = _id_tenant;
$$;


ALTER FUNCTION public.auth_check(_email character varying, _id_tenant bigint) OWNER TO admin;

--
-- Name: layanan_tenant_get_config(bigint, bigint); Type: FUNCTION; Schema: public; Owner: admin
--

CREATE FUNCTION public.layanan_tenant_get_config(_id_user bigint, _id_tenant bigint) RETURNS TABLE(id_user bigint, id_tenant bigint, db_host character varying, db_name character varying, db_port smallint)
    LANGUAGE plpgsql
    AS $$
BEGIN

    return query
        select ua.id_user,
               lt.id_tenant,
               lt.db_host,
               lt.db_name,
               lt.db_port
        from user_account ua
        inner join user_tenant ut on ua.id_user = ut.id_user
        inner join layanan_tenant lt on ut.id_tenant = lt.id_tenant
        where ua.id_user = _id_user and lt.id_tenant = _id_tenant;

END
$$;


ALTER FUNCTION public.layanan_tenant_get_config(_id_user bigint, _id_tenant bigint) OWNER TO admin;

--
-- Name: mn_user_get_by_id_user_raw(bigint); Type: FUNCTION; Schema: public; Owner: admin
--

CREATE FUNCTION public.mn_user_get_by_id_user_raw(_id_user bigint) RETURNS TABLE(id_user bigint, email character varying, password character varying)
    LANGUAGE plpgsql
    AS $$
BEGIN

    return query
        select mu.id_user,
               mu.email,
               mu.password
        from user_account mu
        where mu.id_user = _id_user;

END
$$;


ALTER FUNCTION public.mn_user_get_by_id_user_raw(_id_user bigint) OWNER TO admin;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: layanan_tenant; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.layanan_tenant (
    id_tenant bigint NOT NULL,
    nama_klinik character varying(50),
    db_host character varying(50) NOT NULL,
    db_name character varying(10) NOT NULL,
    db_port smallint
);


ALTER TABLE public.layanan_tenant OWNER TO admin;

--
-- Name: layanan_tenant_id_tenant_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

ALTER TABLE public.layanan_tenant ALTER COLUMN id_tenant ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.layanan_tenant_id_tenant_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: user_account; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_account (
    id_user bigint NOT NULL,
    email character varying(50) NOT NULL,
    password character varying(100) NOT NULL
);


ALTER TABLE public.user_account OWNER TO admin;

--
-- Name: user_account_id_user_seq; Type: SEQUENCE; Schema: public; Owner: admin
--

ALTER TABLE public.user_account ALTER COLUMN id_user ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.user_account_id_user_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: user_tenant; Type: TABLE; Schema: public; Owner: admin
--

CREATE TABLE public.user_tenant (
    id_user bigint NOT NULL,
    id_tenant bigint NOT NULL
);


ALTER TABLE public.user_tenant OWNER TO admin;

--
-- Data for Name: layanan_tenant; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.layanan_tenant (id_tenant, nama_klinik, db_host, db_name, db_port) FROM stdin;
1	klinik pratama	localhost	pis	5432
2	klinik mozambique	localhost	pis	5433
3	klinik nice	localhost	pis	5435
\.


--
-- Data for Name: user_account; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_account (id_user, email, password) FROM stdin;
1	user1@gmail.com	1000.POjK0qlSN+YVWV8QUdSYsw==.n46E74bVdQmqw0vYjhPYg3d14LtjK0Hf2hYtBnJJCAI=
2	user2@gmail.com	1000.6BVcxb334D3TgpMyZSJgIA==.Evx2K+4GtHtuOEcT6iq+EvGQK5q27OHPQdfb/Cwuv3Q=
\.


--
-- Data for Name: user_tenant; Type: TABLE DATA; Schema: public; Owner: admin
--

COPY public.user_tenant (id_user, id_tenant) FROM stdin;
1	1
2	2
2	3
\.


--
-- Name: layanan_tenant_id_tenant_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.layanan_tenant_id_tenant_seq', 3, true);


--
-- Name: user_account_id_user_seq; Type: SEQUENCE SET; Schema: public; Owner: admin
--

SELECT pg_catalog.setval('public.user_account_id_user_seq', 2, true);


--
-- Name: layanan_tenant layanan_tenant_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.layanan_tenant
    ADD CONSTRAINT layanan_tenant_pkey PRIMARY KEY (id_tenant);


--
-- Name: user_account user_account_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_account
    ADD CONSTRAINT user_account_pkey PRIMARY KEY (id_user);


--
-- Name: user_tenant user_tenant_pkey; Type: CONSTRAINT; Schema: public; Owner: admin
--

ALTER TABLE ONLY public.user_tenant
    ADD CONSTRAINT user_tenant_pkey PRIMARY KEY (id_user, id_tenant);


--
-- PostgreSQL database dump complete
--

