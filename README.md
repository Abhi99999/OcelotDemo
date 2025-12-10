# OcelotDemo
step 1 : create required API
		a) when adding identity api update that project program.cs (Here : Identity)
step2: Add ocelot.json without Authentication and update Program.cs(Gateway project) ...
		a)  builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
		b) await app.UseOcelot();
step3: test first whether it is working or not
step4: Add authorization in ocelot.json
step5: Update Gateway program.cs with authorization code
