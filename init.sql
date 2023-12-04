/*==============================================================*/
/* DBMS name:      PostgreSQL 9.x                               */
/* Created on:     04.12.2023 04:03:08                          */
/*==============================================================*/


/*==============================================================*/
/* Table: attends                                               */
/*==============================================================*/
create table attends (
   lesson_id            INT4                 not null,
   client_id            INT4                 not null,
   constraint PK_ATTENDS primary key (lesson_id, client_id)
);

/*==============================================================*/
/* Index: attends_PK                                            */
/*==============================================================*/
create unique index attends_PK on attends (
lesson_id,
client_id
);

/*==============================================================*/
/* Index: attends2_FK                                           */
/*==============================================================*/
create  index attends2_FK on attends (
client_id
);

/*==============================================================*/
/* Index: attends_FK                                            */
/*==============================================================*/
create  index attends_FK on attends (
lesson_id
);

/*==============================================================*/
/* Table: client                                                */
/*==============================================================*/
create table client (
   client_id            SERIAL               not null,
   phone_number         VARCHAR(13)          not null,
   hash_password        VARCHAR(100)         not null,
   balance              DECIMAL(10,2)        not null default 0.00
      constraint CKC_BALANCE_CLIENT check (balance >= 0.00),
   first_name           VARCHAR(16)          not null,
   last_name            VARCHAR(16)          not null,
   birth_date           DATE                 not null,
   gender               BOOL                 not null,
   height               INT4                 not null,
   weight               INT4                 not null,
   constraint PK_CLIENT primary key (client_id)
);

/*==============================================================*/
/* Index: client_PK                                             */
/*==============================================================*/
create unique index client_PK on client (
client_id
);

/*==============================================================*/
/* Table: feedback                                              */
/*==============================================================*/
create table feedback (
   feedback_id          SERIAL               not null,
   client_id            INT4                 not null,
   trainer_id           INT4                 not null,
   rating               DECIMAL(10,1)        not null
      constraint CKC_RATING_FEEDBACK check (rating between 0.0 and 5.0),
   message              VARCHAR(1000)        not null,
   creation_date        DATE                 not null,
   constraint PK_FEEDBACK primary key (feedback_id)
);

/*==============================================================*/
/* Index: feedback_PK                                           */
/*==============================================================*/
create unique index feedback_PK on feedback (
feedback_id
);

/*==============================================================*/
/* Index: leaves_FK                                             */
/*==============================================================*/
create  index leaves_FK on feedback (
client_id
);

/*==============================================================*/
/* Index: receives_FK                                           */
/*==============================================================*/
create  index receives_FK on feedback (
trainer_id
);

/*==============================================================*/
/* Table: lesson                                                */
/*==============================================================*/
create table lesson (
   lesson_id            SERIAL               not null,
   trainer_id           INT4                 not null,
   start_date           DATE                 not null,
   end_date             DATE                 not null,
   constraint PK_LESSON primary key (lesson_id)
);

/*==============================================================*/
/* Index: lesson_PK                                             */
/*==============================================================*/
create unique index lesson_PK on lesson (
lesson_id
);

/*==============================================================*/
/* Index: conducts_FK                                           */
/*==============================================================*/
create  index conducts_FK on lesson (
trainer_id
);

/*==============================================================*/
/* Table: payment                                               */
/*==============================================================*/
create table payment (
   payment_id           SERIAL               not null,
   client_id            INT4                 null,
   service_id           INT4                 null,
   payment_date         DATE                 not null,
   amount               DECIMAL(10,2)        not null,
   constraint PK_PAYMENT primary key (payment_id)
);

/*==============================================================*/
/* Index: payment_PK                                            */
/*==============================================================*/
create unique index payment_PK on payment (
payment_id
);

/*==============================================================*/
/* Index: makes_FK                                              */
/*==============================================================*/
create  index makes_FK on payment (
client_id
);

/*==============================================================*/
/* Index: "consists of_FK"                                      */
/*==============================================================*/
create  index "consists of_FK" on payment (
service_id
);

/*==============================================================*/
/* Table: service                                               */
/*==============================================================*/
create table service (
   service_id           SERIAL               not null,
   type                 VARCHAR(20)          not null,
   name                 VARCHAR(50)          not null,
   price                DECIMAL(10,2)        not null,
   constraint PK_SERVICE primary key (service_id)
);

/*==============================================================*/
/* Index: service_PK                                            */
/*==============================================================*/
create unique index service_PK on service (
service_id
);

/*==============================================================*/
/* Table: subscription                                          */
/*==============================================================*/
create table subscription (
   subscription_id      SERIAL               not null,
   client_id            INT4                 not null,
   name                 VARCHAR(50)          not null,
   purchase_date        DATE                 not null,
   constraint PK_SUBSCRIPTION primary key (subscription_id)
);

/*==============================================================*/
/* Index: subscription_PK                                       */
/*==============================================================*/
create unique index subscription_PK on subscription (
subscription_id
);

/*==============================================================*/
/* Index: has_FK                                                */
/*==============================================================*/
create  index has_FK on subscription (
client_id
);

/*==============================================================*/
/* Table: trainer                                               */
/*==============================================================*/
create table trainer (
   trainer_id           SERIAL               not null,
   phone_number         VARCHAR(13)          not null,
   hash_password        VARCHAR(100)         not null,
   hire_date            DATE                 not null,
   first_name           VARCHAR(16)          not null,
   last_name            VARCHAR(16)          not null,
   birth_date           DATE                 not null,
   gender               BOOL                 not null,
   height               INT4                 not null,
   weight               INT4                 not null,
   constraint PK_TRAINER primary key (trainer_id)
);

/*==============================================================*/
/* Index: trainer_PK                                            */
/*==============================================================*/
create unique index trainer_PK on trainer (
trainer_id
);

/*==============================================================*/
/* Table: visit                                                 */
/*==============================================================*/
create table visit (
   visit_id             SERIAL               not null,
   client_id            INT4                 not null,
   visit_date           DATE                 not null,
   constraint PK_VISIT primary key (visit_id)
);

/*==============================================================*/
/* Index: visit_PK                                              */
/*==============================================================*/
create unique index visit_PK on visit (
visit_id
);

/*==============================================================*/
/* Index: uses_FK                                               */
/*==============================================================*/
create  index uses_FK on visit (
client_id
);

alter table attends
   add constraint FK_ATTENDS_ATTENDS_LESSON foreign key (lesson_id)
      references lesson (lesson_id)
      on delete restrict on update restrict;

alter table attends
   add constraint FK_ATTENDS_ATTENDS2_CLIENT foreign key (client_id)
      references client (client_id)
      on delete restrict on update restrict;

alter table feedback
   add constraint FK_FEEDBACK_LEAVES_CLIENT foreign key (client_id)
      references client (client_id)
      on delete restrict on update restrict;

alter table feedback
   add constraint FK_FEEDBACK_RECEIVES_TRAINER foreign key (trainer_id)
      references trainer (trainer_id)
      on delete restrict on update restrict;

alter table lesson
   add constraint FK_LESSON_CONDUCTS_TRAINER foreign key (trainer_id)
      references trainer (trainer_id)
      on delete restrict on update restrict;

alter table payment
   add constraint "FK_PAYMENT_CONSISTS _SERVICE" foreign key (service_id)
      references service (service_id)
      on delete restrict on update restrict;

alter table payment
   add constraint FK_PAYMENT_MAKES_CLIENT foreign key (client_id)
      references client (client_id)
      on delete restrict on update restrict;

alter table subscription
   add constraint FK_SUBSCRIP_HAS_CLIENT foreign key (client_id)
      references client (client_id)
      on delete restrict on update restrict;

alter table visit
   add constraint FK_VISIT_USES_CLIENT foreign key (client_id)
      references client (client_id)
      on delete restrict on update restrict;

