# AION-Meter-Reloaded
Reprise du projet AION-Meter https://code.google.com/p/aionmeter/ dans le but de rendre l'application utilisable et d'apprendre le C Sharp par la même occasion. Certe, mes connaissances sont moindres, mais je ferai mon possible.

# Première analyse de l'application
* Probablement abandonné depuis le 12.11.2009
* Un certain nombre de d'étape pour que l'application puisse fonctionner ne sont pas accèssible à un utilisateur lambda
* Ne fonctionne pas en lançant AION en français et en anglais, probablement le parser qui ne reconnait pas les nouvelles syntaxes
* L'interface n'est pas complétement traduite et manque d'intuitivité
* Il y a un bug lorsque le chat.log se remplis très vite (instance, prise de forto, gros burst de spiritualiste)
* L'application ne s'occupe pas de crer le fichier system.ovr contenant la syntaxe g_chatlog = "1" activant le logging du tchat en jeu
* Aucune purge du fichier Chat.log prévue dans l'application
* Pour fonctionner, l'application doit être lancé en administrateur, pourquoi ne pas le forcer ?
