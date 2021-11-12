-- DROP DATABASE "EgressDb";
-- CREATE DATABASE "EgressDb";


CREATE TABLE "Authorization"(
    "id" SERIAL NOT NULL,
    "token" VARCHAR(500) NOT NULL,
    "ip_address" VARCHAR(20) NOT NULL,
    "created_date" DATE NOT NULL,
    "refresh_token" VARCHAR(255) NOT NULL,
    "refresh_token_expiry_time" DATE NOT NULL,
    "is_valid" BOOLEAN NOT NULL,
    "user_id" INTEGER NOT NULL
);
ALTER TABLE "Authorization" ADD PRIMARY KEY("id");


CREATE TABLE "User"(
    "id" SERIAL NOT NULL,
    "email" VARCHAR(100) NOT NULL,
    "password" VARCHAR(256) NOT NULL,
    "role" INTEGER NOT NULL,
    "is_validated" BOOLEAN NOT NULL,
    "person_id" INTEGER
);
ALTER TABLE "User" ADD PRIMARY KEY("id");
ALTER TABLE "User" ADD CONSTRAINT "user_email_unique" UNIQUE("email");
ALTER TABLE "User" ADD CONSTRAINT "user_person_id_unique" UNIQUE("person_id");


CREATE TABLE "Person"(
    "id" SERIAL NOT NULL,
    "cpf" VARCHAR(20) NOT NULL,
    "name" VARCHAR(150) NOT NULL,
    "birth_date" DATE NOT NULL,
    "sex" INTEGER NOT NULL,
    "phone_number" VARCHAR(20) NOT NULL,
    "phone_number_2" VARCHAR(20) NULL,
    "perfil_image" VARCHAR(255) NULL,
    "expose_data" BOOLEAN NOT NULL,
    "city" VARCHAR(50) NOT NULL,
    "state" VARCHAR(50) NOT NULL,
    "country" VARCHAR(50) NOT NULL
);
ALTER TABLE "Person" ADD PRIMARY KEY("id");
ALTER TABLE "Person" ADD CONSTRAINT "person_cpf_unique" UNIQUE("cpf");


CREATE TABLE "Course"(
    "id" SERIAL NOT NULL,
    "course_name" VARCHAR(150) NOT NULL
);
ALTER TABLE "Course" ADD PRIMARY KEY("id");
ALTER TABLE "Course" ADD CONSTRAINT "course_course_name_unique" UNIQUE("course_name");


CREATE TABLE "JobAdvertisement"(
    "id" SERIAL NOT NULL,
    "title" VARCHAR(255) NOT NULL,
    "company" VARCHAR(150) NOT NULL,
    "description" TEXT NOT NULL,
    "modality" INTEGER NOT NULL,
    "benefit" TEXT NULL,
    "min_payrange" DECIMAL(8, 2) NULL,
    "max_payrange" DECIMAL(8, 2) NULL,
    "requerements" TEXT NOT NULL,
    "monthly_hours" INTEGER NOT NULL,
    "email" VARCHAR(100) NOT NULL,
    "phone_number" VARCHAR(20) NOT NULL,
    "link" VARCHAR(500) NULL,
    "date_limit" DATE NOT NULL,
    "city" VARCHAR(50) NOT NULL,
    "state" VARCHAR(50) NOT NULL,
    "country" VARCHAR(50) NOT NULL,
    "user_id" INTEGER NOT NULL
);
ALTER TABLE "JobAdvertisement" ADD PRIMARY KEY("id");


CREATE TABLE "News"(
    "id" SERIAL NOT NULL,
    "title" VARCHAR(255) NOT NULL,
    "author" VARCHAR(255) NOT NULL,
    "post_date" DATE NOT NULL,
    "img_src" VARCHAR(255) NULL,
    "content" TEXT NOT NULL,
    "user_id" INTEGER NOT NULL
);
ALTER TABLE "News" ADD PRIMARY KEY("id");


CREATE TABLE "Especialization"(
    "id" SERIAL NOT NULL,
    "course_name" VARCHAR(100) NOT NULL,
    "institution_name" VARCHAR(100) NOT NULL,
    "type" INTEGER NOT NULL,
    "status" INTEGER NOT NULL,
    "modality" INTEGER NOT NULL,
    "start_date" DATE NOT NULL,
    "end_date" DATE NULL,
    "person_id" INTEGER NOT NULL
);
ALTER TABLE "Especialization" ADD PRIMARY KEY("id");


CREATE TABLE "Highlights"(
    "id" SERIAL NOT NULL,
    "title" VARCHAR(255) NOT NULL,
    "description" TEXT NOT NULL,
    "link" VARCHAR(255) NULL,
    "person_id" INTEGER NOT NULL
);
ALTER TABLE "Highlights" ADD PRIMARY KEY("id");


CREATE TABLE "Testimony"(
    "id" SERIAL NOT NULL,
    "content" TEXT NOT NULL,
    "post_date" DATE NOT NULL,
    "person_id" INTEGER NOT NULL
);
ALTER TABLE "Testimony" ADD PRIMARY KEY("id");


CREATE TABLE "Employment"(
    "id" SERIAL NOT NULL,
    "role" VARCHAR(100) NOT NULL,
    "enterprise" VARCHAR(100) NOT NULL,
    "section" VARCHAR(100) NOT NULL,
    "salary" DECIMAL(8, 2) NULL,
    "initiative" INTEGER NOT NULL,
    "status" INTEGER NOT NULL,
    "start_date" DATE NOT NULL,
    "end_date" DATE NOT NULL,
    "person_id" INTEGER NOT NULL
);
ALTER TABLE "Employment" ADD PRIMARY KEY("id");


CREATE TABLE "Area"(
    "course_id" INTEGER NOT NULL,
    "job_id" INTEGER NOT NULL
);
ALTER TABLE "Area" ADD PRIMARY KEY("course_id", "job_id");

CREATE TABLE "Person_Course"(
    "person_id" INTEGER NOT NULL,
    "course_id" INTEGER NOT NULL,
    "start_date" DATE NOT NULL,
    "end_date" DATE NOT NULL,
    "mat" VARCHAR(20) NOT NULL,
    "level" INTEGER NOT NULL,
    "modality" INTEGER NOT NULL,
    "select_course" BOOLEAN NOT NULL
);
ALTER TABLE "Person_Course" ADD PRIMARY KEY("person_id", "course_id");


ALTER TABLE "Authorization" ADD CONSTRAINT "authorization_user_id_foreign" FOREIGN KEY("user_id") REFERENCES "User"("id");
ALTER TABLE "User" ADD CONSTRAINT "user_person_id_foreign" FOREIGN KEY("person_id") REFERENCES "Person"("id");
ALTER TABLE "Highlights" ADD CONSTRAINT "highlights_person_id_foreign" FOREIGN KEY("person_id") REFERENCES "Person"("id");
ALTER TABLE "News" ADD CONSTRAINT "news_user_id_foreign" FOREIGN KEY("user_id") REFERENCES "User"("id");
ALTER TABLE "Especialization" ADD CONSTRAINT "especialization_person_id_foreign" FOREIGN KEY("person_id") REFERENCES "Person"("id");
ALTER TABLE "Testimony" ADD CONSTRAINT "testimony_person_id_foreign" FOREIGN KEY("person_id") REFERENCES "Person"("id");
ALTER TABLE "Employment" ADD CONSTRAINT "employment_person_id_foreign" FOREIGN KEY("person_id") REFERENCES "Person"("id");
ALTER TABLE "JobAdvertisement" ADD CONSTRAINT "jobadvertisement_user_id_foreign" FOREIGN KEY("user_id") REFERENCES "User"("id");

ALTER TABLE "Area" ADD CONSTRAINT "area_course_id_foreign" FOREIGN KEY("course_id") REFERENCES "Course"("id");
ALTER TABLE "Area" ADD CONSTRAINT "area_job_id_foreign" FOREIGN KEY("job_id") REFERENCES "JobAdvertisement"("id");

ALTER TABLE "Person_Course" ADD CONSTRAINT "person_course_person_id_foreign" FOREIGN KEY("person_id") REFERENCES "Person"("id");
ALTER TABLE "Person_Course" ADD CONSTRAINT "person_course_course_id_foreign" FOREIGN KEY("course_id") REFERENCES "Course"("id");
