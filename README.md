 # Aufgabenstellung
 1.) Erstellen Sie mithilfe von PowerShell oder Azure CLI ein App Serice. Dieses App Service soll als Host für das Produktverwaltungsservice verwenden werden
 2.) Entwickeln Sie mithilfe von ASP.NET Core eine Web.API zum Verwalten von Produkten. Die Web.API soll sämtliche CRUD-Operationen unterstützen und als Storage MS SQL Server (InMemory) verwenden. Betreiben Sie die Web.API als Azure App Service
 3.) Sämtliche Operationen müssen in ein Log geschrieben werden. Kontrollieren Sie das Log-Output im Azure Portal
 4.) Jedes Produkt hat eine eindeutige Kennung welche sich aus ProduktId + Suffix (kann über appsettings konfiguriert werden) zusammensetzt. Konfigurieren Sie das Suffix direkt im Azure Portal.
_context.Produkt.Add(p);
_context.SaveChanges();
p.ProduktBezeichnung = p.Id + „Config-Wert“
_context.SaveChanges();
# Theoriefragen
5.) Recherchieren Sie die verschiedenen Möglichkeiten der Skalierung von App Services
       Scale up and scale out (vertikale/horizontale skalierung)
6.) Recherchieren Sie den Begriff „Optimistic Cóncurrency“. Implementieren Sie bei PUT-Vorgängen ein entsprechendes „Update-Conflict-Handling“.
https://docs.microsoft.com/en-us/sql/odbc/reference/develop-app/optimistic-concurrency?view=sql-server-ver15 (optimistic concurrency)
Update Conflict Handling beschreibt die Implementierung einer Businesslogic um Konflikte beim Updaten von Daten zu lösen. Hierbei kann es passieren das User A und B beide einen Wert in die Datenbank schreiben auf Objekt C und hier ein Feld updaten möchten.

