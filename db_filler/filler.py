f = open("sql_empty.txt", "a")

for i in range(1,400):
    f.write(f'INSERT INTO "main"."Actions"("Id","Name","Number","Info") VALUES ({i},"ActionName{i}","Num{i}","Info{i}");\n')
    f.write(f'INSERT INTO "main"."Articles"("Id","Name","ExtendedName","Info") VALUES ({i},"ArticleName{i}","ExtName{i}","Info{i}");\n')
    f.write(f'INSERT INTO "main"."Divisions"("Id","Name","Address","Info") VALUES ({i},"N{i}","Adr{i}","inf{i}");\n')
    f.write(f'INSERT INTO "main"."PersonTypes"("Id","Name","Info") VALUES ({i},"N{i}","I{i}");\n')
    f.write(f'INSERT INTO "main"."Persons"("Id","Name","Info","Phone","Rank","TypeId") VALUES ({i},"Name{i}","Info{i}","Phone{i}","Rank{i}",{i});\n')
    f.write(f'INSERT INTO "main"."Privacies"("Id","Name","Info") VALUES ({i},"Name{i}","Info{i}");\n')
    f.write(f'INSERT INTO "main"."Themes"("Id","Name","Info") VALUES ({i},"Name{i}","Info{i}");\n')
    f.write(f'INSERT INTO "main"."Queries"("Id","Name","Info","Guid","PrivacyId","DivisionId","SignPersonId","TypeId","OuterSecretaryDate","OuterSecretaryNumber","InnerSecretaryNumber","InnerSecretaryDate","CentralSecretaryNumber","CentralSecretaryDate","HasCd","IsVarious","IsEmpty") VALUES ({i},"Name{i}","Info","Guid{i}",1,1,1,1,0,NULL,NULL,0,NULL,0,0,0,0);\n')
    
f.close()