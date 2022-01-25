# Dezvoltarea-Aplicatiilor-Web
Anul 2, Sem 1


  Aplicatia mea Web vine in ajutorul studentilor care doresc sa se inscrie la Facultatea de Matematica si Informatica. Asadar, ei isi vor alege unul sau mai multe cursuri la care sa participe, fiecare curs fiind doar o data pe saptamana, pot trece pe rand prin fiecare an de facultate, se pot caza la caminul facultatii, in fiecare camera cate un singur student.<br/><br/>


  Relatii intre entitati:<br/>
:heavy_check_mark: Studenti - CamereCamin 1:1

:heavy_check_mark: Cursuri - ZiCurs  1:M

:heavy_check_mark: Studenti-Cursuri M:M -> Catalog
 
 
<br/>
  Am implementat:
  
In backend:

  :heavy_check_mark: 3 controllere cu metode CRUD
  
  :heavy_check_mark: Metode din Linq: OrderBy, Select, Where, Join (vezi StudentiManager)
  
  :heavy_check_mark: Repository pattern
  
  
 In frontend:
 
  :heavy_check_mark: Cel putin 3 componente: cursuri, dashboard, course-detail, message
  
  :heavy_check_mark: Servicii pentru curs (conectat la .Net) si message
  
  :heavy_check_mark: Module pentru routing, forms, httpClient
  
  :heavy_check_mark: Form
  
  :heavy_check_mark: Directiva 
