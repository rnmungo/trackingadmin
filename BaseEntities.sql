select * from Trucks;
select * from Locations;
select * from Distances;
select * from RoadMaps;
select * from Travels;

DBCC CHECKIDENT ('RoadMaps', RESEED, 1000000);

insert into
	Trucks (Id, LicensePlate, Model, CreatedAt)
values
	(cast('0FEE1FA5-DBC8-4B3B-9A80-446D5C2BEBDB' as uniqueidentifier), 'A12 3BDC', 'Scania G380', getdate()),
	(cast('39840222-2FA8-4427-9C94-3A44619B9A81' as uniqueidentifier), 'A52 3SGC', 'Mercedez Benz Zetros', getdate()),
	(cast('0EC977FB-5DF3-4FB9-933B-C5486427D0CF' as uniqueidentifier), 'F35 4FBD', 'Iveco Stralis', getdate()),
	(cast('22A6DC04-0B62-4513-BCBC-51850B8E938E' as uniqueidentifier), 'S51 6SFN', 'Iveco Cursor', getdate()),
	(cast('6FF95E6F-E1DB-499F-8C23-D3DA78ED3237' as uniqueidentifier), 'S65 2SDJ', 'Scania R470', getdate()),
	(cast('7CE3D30F-A953-4219-9CEE-A26E54036A95' as uniqueidentifier), 'D34 6DFG', 'Scania R470', getdate());

insert into Locations
	(Id, Name, Latitude, Longitude, CreatedAt)
values
	(cast('CF037E22-8617-4ADC-AFAD-23C7B5FFC41E' as uniqueidentifier), 'Jujuy (S.S. de Jujuy)', -24.0167, -65.4167, getdate()),
	(cast('7F36007D-80E0-4373-9986-C13D3A664EFD' as uniqueidentifier), 'Salta (Salta)', -24.8, -65.4167, getdate()),
	(cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), 'Formosa (Formosa)', -26.1833, -58.3, getdate()),
	(cast('76857610-BF9C-4B34-91F0-75B89F9B3A7A' as uniqueidentifier), 'Chaco (Resistencia)', -27.45, -58.85, getdate()),
	(cast('E3E1572D-9D2D-4849-8D57-78A3FA38C0E4' as uniqueidentifier), 'Catamarca (Catamarca)', -28.4833, -65.7833, getdate()),
	(cast('B2095F10-FA91-47A3-9487-2B9DCA82C0AB' as uniqueidentifier), 'Tucumán (S. M. de Tuc.)', -26.85, -65.2, getdate()),
	(cast('0893C1AA-1CBA-416B-AE0C-09ED43CD196D' as uniqueidentifier), 'Santiago del Estero', -27.75, -64.2667, getdate()),
	(cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), 'Corrientes', -27.45, -58.8167, getdate()),
	(cast('AC4D1DA1-FF7A-42DF-ACB0-A8A15D21B3FD' as uniqueidentifier), 'Misiones (Posadas)', -27.3167, -55.8833, getdate()),
	(cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), 'La Rioja', -29.4, -66.8333, getdate()),
	(cast('D3EF4E90-0866-41D6-80A0-592C734CD2EA' as uniqueidentifier), 'Santa Fe (S. Fe)', -31.7, -60.7667, getdate()),
	(cast('82A8DBB8-FF90-4034-9231-036EEC63BA60' as uniqueidentifier), 'San Juan', -31.5167, -68.55, getdate()),
	(cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), 'Córdoba', -31.35, -64.0833, getdate()),
	(cast('68A9C693-B9B9-4B81-A1A6-7BA3E3B2BF37' as uniqueidentifier), 'Entre Ríos (Concordia)', -31.3667, -58.15, getdate()),
	(cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), 'Mendoza', -32.8667, -68.85, getdate()),
	(cast('1762AD9C-784F-4835-864D-FA8C5EC257BD' as uniqueidentifier), 'San Luis', -33.2833, -66.3667, getdate()),
	(cast('D316560F-DCC3-4A3F-8D3D-310C74E74B15' as uniqueidentifier), 'La Pampa (S. Rosa)', -36.65, -64.2833, getdate()),
	(cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), 'Buenos Aires', -34.4667, -58.4667, getdate()),
	(cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), 'Neuquén', -38.9167, -68.0833, getdate()),
	(cast('0A1E810F-3F66-4E58-A8CA-E560B2F6AD7D' as uniqueidentifier), 'Río Negro (Viedma)', -40.8167, -63, getdate()),
	(cast('29CE3FA9-A5E4-42D8-96FA-505C92039BF8' as uniqueidentifier), 'Chubut (C. Rivadavia)', -45.85, -67.4667, getdate()),
	(cast('9D81BF70-9CB1-4939-95F2-4F481801C8E6' as uniqueidentifier), 'Santa Cruz (R. Gallegos)', -51.6167, -69.2333, getdate()),
	(cast('99467C58-2D16-461B-8657-5A7AA58FCB81' as uniqueidentifier), 'Tierra del fuego (Ushuaia)', -54.8, -68.2833, getdate()),
	(cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), 'La Plata', -34.92145, -57.95453, getdate());

insert into Distances
	(OriginLocationId,  DestinationLocationId, DistanceInKm, CreatedAt)
values
	-- Córdoba
	(cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), 646, getdate()),
	(cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), 0, getdate()),
	(cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), 677, getdate()),
	(cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), 824, getdate()),
	(cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), 698, getdate()),
	(cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), 340, getdate()),
	(cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), 466, getdate()),
	(cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), 907, getdate()),
	-- Buenos Aires
	(cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), 0, getdate()),
	(cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), 646, getdate()),
	(cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), 792, getdate()),
	(cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), 933, getdate()),
	(cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), 53, getdate()),
	(cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), 986, getdate()),
	(cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), 985, getdate()),
	(cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), 989, getdate()),
	-- Corrientes
	(cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), 792, getdate()),
	(cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), 677, getdate()),
	(cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), 0, getdate()),
	(cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), 157, getdate()),
	(cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), 830, getdate()),
	(cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), 814, getdate()),
	(cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), 1131, getdate()),
	(cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), 1534, getdate()),
	-- Formosa
	(cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), 933, getdate()),
	(cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), 824, getdate()),
	(cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), 157, getdate()),
	(cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), 0, getdate()),
	(cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), 968, getdate()),
	(cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), 927, getdate()),
	(cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), 1269, getdate()),
	(cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), 1690, getdate()),
	-- La Plata
	(cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), 53, getdate()),
	(cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), 698, getdate()),
	(cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), 830, getdate()),
	(cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), 968, getdate()),
	(cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), 0, getdate()),
	(cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), 1038, getdate()),
	(cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), 1029, getdate()),
	(cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), 1005, getdate()),
	-- La Rioja
	(cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), 986, getdate()),
	(cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), 340, getdate()),
	(cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), 814, getdate()),
	(cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), 927, getdate()),
	(cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), 1038, getdate()),
	(cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), 0, getdate()),
	(cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), 427, getdate()),
	(cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), 1063, getdate()),
	-- Mendoza
	(cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), 985, getdate()),
	(cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), 466, getdate()),
	(cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), 1131, getdate()),
	(cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), 1269, getdate()),
	(cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), 1029, getdate()),
	(cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), 427, getdate()),
	(cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), 0, getdate()),
	(cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), 676, getdate()),
	-- Neuquén
	(cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), cast('0382533C-C9D2-424B-9BF2-8C3CF735E028' as uniqueidentifier), 989, getdate()),
	(cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), cast('E5E942BE-9746-43FE-BA53-641D9D55E1B5' as uniqueidentifier), 907, getdate()),
	(cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), cast('273F19F2-9617-42A3-8013-2CFED9EA4DAC' as uniqueidentifier), 1534, getdate()),
	(cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), cast('CE0634D2-4CAA-4146-AEC1-2F1508A4317C' as uniqueidentifier), 1690, getdate()),
	(cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), cast('39261484-640B-4DE7-926E-42170845444B' as uniqueidentifier), 1005, getdate()),
	(cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), cast('D7ED63E7-8E30-4310-841D-270EA945E702' as uniqueidentifier), 1063, getdate()),
	(cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), cast('4CBF0913-21D8-4B6F-9FA3-B617A84EE76F' as uniqueidentifier), 676, getdate()),
	(cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), cast('56642316-57FA-4B78-BB98-C094BE9D1F60' as uniqueidentifier), 0, getdate());