# OrdOnline
- Membres du groupe : Baptiste BALBI, Daiyan COSTILHES, Samuel DELASSUS, Antoine LACHAUD, Sébastien LATRONCHE

# Table des matières
1. [Description](#Description)
2. [Pré-requis](#Pré-requis)
3. [Installation](#Installation)
4. [Setup de la BDD](#Setup de la BDD)
5. [Démarrer l'application](#Démarrer l'application)
6. [Utiliser l'application](#Utiliser l'application)

# Description
Application WPF C# permettant de générer, d'envoyer, et de récupérer des ordonnances dématérialisées.
Afin de développer cette application, nous avons utilisé :
* [Visual Studio 2022](https://visualstudio.microsoft.com/fr/vs/) - IDE
* [MySQL](https://www.mysql.com/fr/) - Outil Open Source de gestion de base de données (BDD)

# Pré-requis
OrdOnline fonctionne uniquement sous Windows 7, 10, ou 11.
Vous devez posséder sur votre machine les outils suivants :
* [MySQL Workbench](https://www.mysql.com/fr/products/workbench/) - Pour compiler et exécuter la base de données
* [.NET Framework 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) - Pour exécuter le logiciel
* (OPTIONNEL)[Visual Studio 2022](https://visualstudio.microsoft.com/fr/vs/) - Si vous souhaitez compiler le projet vous-même

# Installation
Téléchargez la dernière version du dossier ZIP présente dans l'onglet RELEASES et extractez-le où vous le souhaitez.

# Setup de la BDD
Afin de paramétrer la base de données, vous devez utiliser le fichier mastercamp.sql joint dans le dossier ZIP. Vous pouvez soit copier-coller le script et l'exécuter dans MySQL Workbench, ou directement ouvrir et exécuter le fichier dans le même logiciel.
Pour éviter des erreurs de connexion au démarrage de l'application, renseignez les informations de votre BDD dans le fichier config.toml joint dans le dossier ZIP.

# Démarrer l'application
Pour démarrer l'application, exécutez le fichier MastercampProjectG139.exe joint dans le dossier ZIP. Ne supprimez ni modifiez aucune DLL.
Utilisez les identifiants dans la section ci-après pour vous connecter et accéder aux différentes vues (médecin ou pharmacien) selon l'utilisateur que vous utiliserez.

# Utiliser l'application
Vous devrez vous connecter lorsque l'application aura démarrée. Les identifiants des 5 personnels de santé de la BDD sont :
-   Numéro SS   |  MDP   |  Métier | Prénom NOM
- 1334527161492 | admin1 | MEDECIN | Samuel DELASSUS
- 4341982456785 | admin2 | MEDECIN | Baptiste BALBI
- 1346248675312 | admin3 | PHARMAC | Antoine LACHAUD
- 4364512346521 | admin4 | MEDECIN | Daiyan COSTILHES
- 6153245687521 | admin5 | PHARMAC | Sébastien LATRONCHE
