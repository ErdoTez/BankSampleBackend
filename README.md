# BankSampleBackend

PostgreSql kullanılmıştır

Tablo için ; 

-- bank_sample."CardTransactionInfo" definition

-- Drop table

-- DROP TABLE bank_sample."CardTransactionInfo";

CREATE TABLE bank_sample."CardTransactionInfo" (
	"Id" bigserial NOT NULL,
	"CreatedBy" int8 NOT NULL,
	"CreatedAt" timestamp NOT NULL,
	"UpdatedBy" int8 NOT NULL,
	"UpdatedAt" timestamp NOT NULL,
	"TransactionId" int8 NOT NULL,
	"CardholderName" varchar(500) NOT NULL,
	"Pan" varchar(16) NOT NULL,
	"ExpiryDate" int8 NOT NULL,
	"Amount" numeric NOT NULL,
	"CardType" int8 NULL,
	CONSTRAINT "CardTransactionInfo_pk" PRIMARY KEY ("Id")
);


Angular 14 de gelen 15 de stabil olan standlone componenent kullanmadım.LazyLoading module olarak devam ettim.Tek bir page bulunmaktadir

