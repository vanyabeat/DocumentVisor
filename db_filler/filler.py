
for i in range(300,400):
    print(f'INSERT INTO "main"."PersonTypes"("Id","Name","Info") VALUES ({i},"Name{i}","info{i}");')