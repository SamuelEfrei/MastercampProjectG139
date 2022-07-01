DROP DATABASE IF EXISTS Mastercamp;
create database Mastercamp;
use Mastercamp;


CREATE TABLE PersonnelSante(
   idPS INT,
   prenom VARCHAR(50) NOT NULL,
   nom VARCHAR(50) NOT NULL,
   numSSPersonnel BIGINT NOT NULL,
   mdp VARCHAR(250) NOT NULL,
   PRIMARY KEY(idPS)
);

insert into PersonnelSante values ( 1, "samuel", "delassus", 1334527161492, "$2a$11$rOePjQeTHMqEHJ0x6BI/8uxrWu4qEc6F1mzPqyvDuNIaIOMC98Fky");
insert into PersonnelSante values ( 2, "baptiste", "balbi", 4341982456785, "$2a$11$GT5BjPPKvVqrOAhgMPoKg.LIXaMxaNozk2sMqTdk9.qsyrxIAwuZu");
insert into PersonnelSante values ( 3, "antoine", "lachaud", 1346248675312, "$2a$11$q18QtJovREITPFxwSM0/buHJmdcQZtlI/VAJ4.n8uEuO2ze0JhEFG");
insert into PersonnelSante values ( 4, "daiyan", "costilhes", 4364512346521, "$2a$11$xV0WONXrSJYXibXDUY9HGemTvapa73pVIAquZDlOd8QFk.pSbrT7u");
insert into PersonnelSante values ( 5, "sébastien", "latronche", 6153245687521, "$2a$11$jw52E8EggbrAMnbIPL5xV.EKa2RlLvN3x1Ae8e6HPs5KD409cwEua");

CREATE TABLE Medicament(
   idMedic INT,
   nom VARCHAR(50) NOT NULL,
   dangereux BOOLEAN NOT NULL,
   PRIMARY KEY(idMedic)
);

insert into Medicament values (1, 'Doliprane', false);
insert into Medicament values (2, 'Advil', false);
insert into Medicament values (3, 'Dafalgan', false);
insert into Medicament values (4, 'Efferalgan', false);
insert into Medicament values (5, 'Spasfon', false);
insert into Medicament values (6, 'Glucosamine', true);
insert into Medicament values (7, 'Diclofénac', true);
insert into Medicament values (8, 'Bupropione', true);
insert into Medicament values (9, 'CBD', false);
insert into Medicament values (10, 'Morphine', true);

CREATE TABLE Medecin(
   idMedecin VARCHAR(50),
   idPS INT NOT NULL,
   PRIMARY KEY(idMedecin),
   UNIQUE(idPS),
   FOREIGN KEY(idPS) REFERENCES PersonnelSante(idPS)
);

insert into Medecin values (1,1);
insert into Medecin values (2,2);
insert into Medecin values (3,4);

CREATE TABLE Pharmacien(
   idPharma VARCHAR(50),
   idPS INT NOT NULL,
   PRIMARY KEY(idPharma),
   UNIQUE(idPS),
   FOREIGN KEY(idPS) REFERENCES PersonnelSante(idPS)
);

insert into Pharmacien values (1,3);
insert into Pharmacien values (2,5);

CREATE TABLE Ordonnance(
   idOrdo INT,
   codeSecret INT NOT NULL,
   numSSPatient BIGINT NOT NULL,
   idPharma VARCHAR(50) NOT NULL,
   idMedecin VARCHAR(50) NOT NULL,
   PRIMARY KEY(idOrdo),
   FOREIGN KEY(idPharma) REFERENCES Pharmacien(idPharma),
   FOREIGN KEY(idMedecin) REFERENCES Medecin(idMedecin)
);

insert into Ordonnance values (1, 000000, 4364512336521, 1, 1);
insert into Ordonnance values (2, 000001, 4364122336521, 2, 2);

CREATE TABLE MedicamentOrdonnance(
   idOrdo INT,
   idMedic INT,
   dureeMedicament VARCHAR(200),
   quantiteParJour VARCHAR(200),
   PRIMARY KEY(idOrdo, idMedic),
   FOREIGN KEY(idOrdo) REFERENCES Ordonnance(idOrdo),
   FOREIGN KEY(idMedic) REFERENCES Medicament(idMedic)
);

insert into MedicamentOrdonnance values (1, 1, 'pendant 7 ans', '87 par jour');
insert into MedicamentOrdonnance values (2, 10, 'pendant 1 mois', '4Litre par jour');
insert into MedicamentOrdonnance values (2, 8, 'pendant 2 semaines', '7 par jour');