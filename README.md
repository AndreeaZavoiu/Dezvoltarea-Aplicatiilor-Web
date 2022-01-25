# Dezvoltarea-Aplicatiilor-Web
Anul 2, Sem 1


  Aplicatia mea Web vine in ajutorul studentilor care doresc sa se inscrie la Facultatea de Matematica si Informatica. Asadar, ei isi vor alege unul sau mai multe cursuri la care sa participe, fiecare curs fiind doar o data pe saptamana, pot trece pe rand prin fiecare an de facultate, se pot caza la caminul facultatii, in fiecare camera cate un singur student.


  Relatii intre entitati:
  
Studenti - CamereCamin 1:1

Cursuri - ZiCurs  1:M

Studenti-Cursuri M:M -> Catalog
 
 
 
  Am implementat:
  
In backend:

  3 controllere cu metode CRUD
  
  Metode din Linq: OrderBy, Select, Where, Join (vezi StudentiManager)
  
  Repository pattern
  
  
 In frontend:
 
  Cel putin 3 componente: cursuri, dashboard, course-detail, message
  
  Servicii pentru curs (conectat la .Net) si message
  
  Module pentru routing, forms, httpClient
  
  Form
  
  Directiva 
