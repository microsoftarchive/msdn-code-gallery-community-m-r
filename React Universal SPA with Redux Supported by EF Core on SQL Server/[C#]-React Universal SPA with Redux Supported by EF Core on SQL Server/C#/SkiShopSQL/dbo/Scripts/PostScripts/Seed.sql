
IF NOT EXISTS (SELECT TOP 1 CategoryId FROM Categories) 
	BEGIN
		INSERT INTO Categories (CategoryId, CategoryName )
			VALUES (1, 'Backcountry'), (2, 'Cross-Country'), (3, 'Downhill');

		INSERT INTO Countries (CountryId, CountryName)
			VALUES (1, 'USA'), (2, 'Canada'), (3, 'Czech'), (4, 'Germany'), (5, 'Ukraine'), (6, 'Austria'),
				(7, 'China'), (8, 'Spain'), (9, 'Bulgaria');

		INSERT INTO Brands (BrandId, BrandName, CountryId)
			VALUES (1, 'Rossignol', 8), (2, 'Atomic', 6), (3, 'DPS', 1), (4, 'Voile', 1), (5, 'Komperdell', 3),
				(6, 'Volkl', 4), (7, 'Fischer', 5), (8, 'Salomon', 6), (9, 'La Sportiva', 1), (10, 'Foon Skis', 2),
				(11, 'K2', 7), (12, 'Head', 3), (13, 'G3', 7);

		INSERT INTO Genders (GenderId, GenderName) 
			VALUES (1, 'Men''s'), (2, 'Women''s'), (3, 'Unisex'), (4, 'Children'), 
				(5, 'Children to Youth'), (6, 'Youth');

		INSERT INTO Styles (StyleId, StyleNo, StyleName, BrandId, CategoryId, GenderId, ImageBig, ImageSmall, 
			PriceCurrent, PriceRegular) 
			values (1, '123456', 'X-ium Skating NIS Skis', 1, 2, 3, '/image/skis/123456big.jpg', 
					'/image/skis/123456small.jpg', 365.00, 489.00),
				(2, '323465', 'X-Pro SW Skis With Bindings', 8, 3, 1, '/image/skis/323465big.jpg', 
					'/image/skis/323465small.jpg', 579.00, 799.00),
				(3, '323464', 'W-Pro SW Skis With Bindings', 8, 3, 2, '/image/skis/323464big.jpg',
					'/image/skis/323464small.jpg', 549.00, 799.00),
				(4, '323463', 'RTM 78 Skis + Bindings', 6, 3, 1, '/image/skis/323463big.jpg',
					'/image/skis/323463small.jpg', 674.00, 899.00),
				(5, '323462', 'Supershape i-Rally Skis + Bindings', 12, 3, 3, '/image/skis/323462big.jpg', 
					'/image/skis/323462small.jpg', 899.00, 1199.00),
				(6, '323461', 'Flair SC Skis + Bindings', 6, 3, 2, '/image/skis/323461big.jpg',
					'/image/skis/323461small.jpg', 674.00, 899.00),
				(7, '323460', 'Ski Set', 5, 3, 4, '/image/skis/323460big.jpg', 
					'/image/skis/323460small.jpg', 55.00, 55.00),
				(8, '223466', 'Empire 115 Skis ', 13, 1, 3, '/image/skis/223466big.jpg', 
					'/image/skis/223466small.jpg', 699.00, 949.00),
				(9, '223465', 'Talkback 96 Skis', 11, 1, 2, '/image/skis/223465big.jpg',
					'/image/skis/223465small.jpg', 699.00, 699.00),
				(10, '223464', 'Wailer 106 Tour1 Skis', 3, 1, 1, '/image/skis/223464big.jpg',
					'/image/skis/223464small.jpg', 1099.00, 1099.00),
				(11, '223463', 'Gretski Skis', 10, 1, 3, '/image/skis/223463big.jpg', 
					'/image/skis/223463small.jpg', 1149.00, 1149.00),
				(12, '323466', 'Total Joy Skis + Bindings', 12, 3, 2, '/image/skis/323466big.jpg', 
					'/image/skis/323466small.jpg', 599.00, 799.00),
				(13, '223462', 'V-Werks BMT 122 Skis', 6, 1, 3, '/image/skis/223462big.jpg', 
					'/image/skis/223462small.jpg', 899.00, 1199.00), 
				(14, '223460', 'Vapor Svelte Skis', 9, 1, 3, '/image/skis/223460big.jpg', 
					'/image/skis/223460small.jpg', 1299.00, 1299.00),
				(15, '223459', 'Super Charger Skis', 4, 1, 2, '/image/skis/223459big.jpg', 
					'/image/skis/223459small.jpg', 938.00, 938.00),
				(16, '223458', 'Wailer 99 Tour1 Skis', 3, 1, 1, '/image/skis/223458big.jpg', 
					'/image/skis/223458small.jpg', 1099.00, 1099.00),
				(17, '123463', 'Snowscape 7 Siam Skis', 8, 2, 2, '/image/skis/123463big.jpg', 
					'/image/skis/123463small.jpg', 189.00, 189.00),
				(18, '123462', 'SC Skate NIS Skis', 7, 2, 3, '/image/skis/123462big.jpg', 
					'/image/skis/123462small.jpg', 186.00, 249.00),
				(19, '123461', 'Equipe 8 Skate Skis', 8, 2, 3, '/image/skis/123461big.jpg', 
					'/image/skis/123461small.jpg', 279.00, 279.00),
				(20, '123460', 'SCS Skate NIS Skis', 7, 2, 3, '/image/skis/123460big.jpg', 
					'/image/skis/123460small.jpg', 287.00, 384.00), 
				(21, '123459', 'Delta Course Classic NIS Skis', 1, 2, 3, '/image/skis/123459big.jpg', 
					'/image/skis/123459small.jpg', 299.00, 399.00), 
				(22, '123458', 'E99 Easy Skin Xtralite Skis', 7, 2, 3, '/image/skis/123458big.jpg', 
					'/image/skis/123458small.jpg', 359.00, 449.00), 
				(23, '123457', 'Sport Pro Skintec Skis', 2, 2, 3, '/image/skis/123457big.jpg', 
					'/image/skis/123457small.jpg', 279.00, 399.00), 
				(24, '223461', 'Superneo Skis', 10, 1, 3, '/image/skis/223461big.jpg', 
					'/image/skis/223461small.jpg', 1199.00, 1199.00), 
				(25, '323467', 'Strong Instinct Ti Skis + Bindings', 12, 3, 1, '/image/skis/323467big.jpg', 
					'/image/skis/323467small.jpg', 559.00, 749.00);

			INSERT INTO IdealFors (IdealForId, IdealForSpec)
				VALUES (1, 'Fitness skate skiing'), (2, 'Skate ski racing'), (3, 'Recreational classic skiing'),
					(4, 'Fitness classic skiing'), (5, 'Alpine touring'), (6, 'Ski mountaineering'), 
					(7, 'Freeride and freeskiing'), (8, 'Downhill skiing'), (9, 'Off-track touring'), 
					(10, 'Classic ski racing'), (11, 'Backcountry skiing');
					
			INSERT INTO StyleIdealFors (StyleId, IdealForId)
				VALUES (1, 2), (2, 8), (3, 8), (4, 8), (5, 8), (6, 8), (7, 8), (8, 5), (8, 7), (9, 5), (9, 6), (10, 11),
					(11, 5), (11, 6), (12, 8), (13, 5), (13, 7), (14, 5), (15, 5), (16, 5), (16, 6), (16, 7), (17, 3), 
					(17, 4), (18, 1), (19, 2), (20, 1), (20, 2), (21, 4), (22, 10), (23, 3), (23, 4), (24, 5), (24, 7),
					(25, 8);

			INSERT INTO Skus (SkuId, SkuNo, StyleId, Size, Quantity)
				VALUES (1, '12345601', 1, '173cm', 2), (2, '32346002', 7, '70cm', 3), (3, '32346001', 7, '60cm', 5),
					(4, '22346605', 8, '190cm', 1), (5, '22346604', 8, '185cm', 0), (6, '22346603', 8, '180cm', 0), 
					(7, '22346602', 8, '175cm', 2), (8, '22346601', 8, '170cm', 1), (9, '22346502', 9, '170cm', 3),
					(10, '22346501', 9, '163cm', 2), (11, '22346403', 10, '185cm', 0), (12, '22346402', 10, '178cm', 3),
					(13, '22346401', 10, '168cm', 2), (14, '22346302', 11, '188cm', 2), (15, '22346301', 11, '175cm', 4),
					(16, '22346202', 13, '186cm', 0), (17, '22346201', 13, '176cm', 2), (18, '22346104', 24, '185cm', 1),
					(19, '32346704', 25, '177cm', 0), (20, '22346102', 24, '165cm', 5), (21, '22346101', 24, '158cm', 3),
					(22, '22346003', 14, '188cm', 1), (23, '32346101', 6, '153cm', 3 ), (24, '32346102', 6, '158cm', 5),
					(25, '32346103', 6, '163cm', 4), (26, '32346201', 5, '156cm', 3), (27, '32346703', 25, '170cm', 1),
					(28, '32346702', 25, '163cm', 3), (29, '32346701', 25, '156cm', 2), (30, '32346605', 12, '168cm', 2),
					(31, '32346604', 12, '163cm', 3), (32, '32346603', 12, '158cm', 3), (33, '32346602', 12, '153cm', 0),
					(34, '32346601', 12, '148cm', 2), (35, '32346504', 2, '176cm', 3), (36, '32346503', 2, '169cm', 2),
					(37, '22346002', 14, '178cm', 2), (38, '32346502', 2, '162cm', 0), (39, '32346403', 3, '162cm', 5),
					(40, '32346402', 3, '155cm', 3), (41, '32346401', 3, '148cm', 2), (42, '32346304', 4, '177cm', 0),
					(43, '32346303', 4, '170cm', 2), (44, '32346302', 4, '163cm', 3), (45, '32346301', 4, '156cm', 0),
					(46, '32346204', 5, '177cm', 1), (47, '32346203', 5, '170cm', 2), (48, '32346202', 5, '163cm', 5),
					(49, '32346501', 2, '155cm', 2), (50, '22346001', 14, '168cm', 3), (51, '22346103', 24, '175cm', 2),
					(52, '12346003', 20, '182cm', 2), (53, '12346103', 19, '186cm', 0), (54, '12346102', 19, '179cm', 2),
					(55, '12346101', 19, '174cm', 3), (56, '12345705', 23, '208cm', 1), (57, '12346005', 20, '192cm', 0),
					(58, '12346004', 20, '187cm', 1), (59, '12345801', 22, '180cm', 2), (60, '22345902', 15, '164cm', 2),
					(61, '12346104', 19, '191cm', 1), (62, '12346002', 20, '177cm', 0), (63, '12345802', 22, '185cm', 0),
					(64, '12345904', 21, '206cm', 1), (65, '12345903', 21, '201cm', 0), (66, '12345902', 21, '196cm', 2),
					(67, '12345803', 22, '190cm', 2), (68, '12345901', 21, '186cm', 3), (69, '12345804', 22, '195cm', 3),
					(70, '12345807', 22, '210cm', 1), (71, '12346001', 20, '172cm', 2), (72, '12345806', 22, '205cm', 2),
					(73, '12345805', 22, '200cm', 0), (74, '12346201', 18, '172cm', 3), (75, '22345901', 15, '154cm', 3),
					(76, '12345602', 1, '180cm', 3), (77, '22345803', 16, '184cm', 2), (78, '12345603', 1, '186cm', 4),
					(79, '22345802', 16, '176cm', 3), (80, '22345801', 16, '168cm', 1), (81, '12345604', 1, '192cm', 2),
					(82, '12346303', 17, 'Large', 0), (83, '12345704', 23, '200cm', 2), (84, '12346302', 17, 'Medium', 2),
					(85, '12345701', 23, '176cm', 1), (86, '12346205', 18, '192cm', 0), (87, '12345702', 23, '184cm', 2),
					(88, '12346204', 18, '187cm', 2), (89, '12346203', 18, '182cm', 0), (90, '12346202', 18, '177cm', 2),
					(91, '12345703', 23, '192cm', 3), (92, '12346301', 17, 'Small', 3);

			INSERT INTO dbo.Provinces(ProvinceCode, ProvinceName, CountryId)
				VALUES ('AB', 'Alberta', 2), ('BC', 'British Columbia', 2), ('MB', 'Manitoba', 2), 
					('NB', 'New Brunswick', 2), ('NL', 'Newfoundland and Labrador', 2), ('NS', 'Nova Scotia', 2),
					('NT', 'Northwest Territories', 2), ('NU', 'Nunavut', 2), ('ON', 'Ontario', 2),
					('PE', 'Prince Edward Island', 2), ('QC', 'Quebec', 2), ('SK', 'Saskatchewan', 2), 
					('YT', 'Yukon', 2);
			
			DECLARE @userId TABLE(UserId INT);
			INSERT INTO dbo.UserIdentities(Email, LastName, FirstName, ScreenName)
				OUTPUT inserted.UserId INTO @userId
				VALUES ('Alice.Brown@example.com', 'Brown', 'Alice', 'Alice789'),
				('Ann.Blare@example.com', 'Blare', 'Ann', 'Ann123'),
				('Cathy.Jones@example.com', 'Jones', 'Cathy', 'Cathy456'),
				('John.Miller@example.com', 'Miller', 'John', 'John123');


			DECLARE @orderId TABLE(OrderId INT);
			INSERT INTO dbo.Orders (CustomerOrderId, Email, UserId, FullName, ProvinceId, City, AddressLine, 
				PostalCode, TotalValue, CreatedDateTime)
				OUTPUT inserted.OrderId INTO @orderId
				VALUES ('E3EF6231-843F-4D07-9C90-E4F09E2B5AC2', 'Alice.Brown@example.com', 
					(SELECT TOP(1) UserId FROM dbo.UserIdentities WHERE Email = 'Alice.Brown@example.com'), 
					'Alice Brown', 1, 'Calgary', 
					'77 10th Ave SW', 'T2R 0A9', 365.00, GETDATE());

			INSERT INTO dbo.OrderItems(OrderId, SkuId, Price, Quantity)
				VALUES ((SELECT TOP (1) OrderId FROM @orderId), (SELECT SkuId FROM dbo.Skus WHERE SkuNo = '12345603'), 365.00, 1);


            INSERT INTO dbo.Reviews(UserId, StyleId, Rating, ReviewText, CreatedDateTime, CreatedDateTimeUTC)
				VALUES((SELECT UserId from dbo.UserIdentities WHERE ScreenName = 'Alice789'), (SELECT StyleId FROM dbo.Styles WHERE StyleNo = '223465'), 
					4, 'Good backcountry skis', GETDATE(), GETUTCDATE());

			INSERT INTO dbo.Reviews(UserId, StyleId, Rating, ReviewText, CreatedDateTime, CreatedDateTimeUTC)
				VALUES((SELECT UserId FROM dbo.UserIdentities WHERE ScreenName = 'John123'), (SELECT StyleId FROM dbo.Styles WHERE StyleNo = '123463'),
					5, 'I''m new to skiing and wanted to start classic cross country skiing, so I bought these skis and Salomon bindings and boots and poles. The skis look good, the instructions were relatively clear and simple to understand, and I was able to install the bindings in 15 minutes. On the snow, I found them comfortable to use and easy enough to get used to. I''m pretty happy with the purchase!',
					'10/7/2017 4:52:43 PM', '10/7/2017 11:52:43 PM');

			INSERT INTO dbo.Reviews(UserId, StyleId, Rating, ReviewText, CreatedDateTime, CreatedDateTimeUTC)
				VALUES((SELECT UserId FROM dbo.UserIdentities WHERE ScreenName = 'Ann123'), (SELECT StyleId FROM dbo.Styles WHERE StyleNo = '123463'),
					5, 'The peaceful feeling you''ll get during a backcountry adventure on the Salamon Snowscape 7 Siam Ski is something that really can''t be beat. Except if you factor in the fact you''re in for one of the most rigorous, calorie-crushing workouts possible. Get your outfit on, lace up the boots, and click-in and lose yourself in the moment.',
					'1/5/2017 8:52:43 AM', '1/5/2017 4:52:43 PM');

			INSERT INTO dbo.Reviews(UserId, StyleId, Rating, ReviewText, CreatedDateTime, CreatedDateTimeUTC)
				VALUES((SELECT UserId FROM dbo.UserIdentities WHERE ScreenName = 'Cathy456'), (SELECT StyleId FROM dbo.Styles WHERE StyleNo = '123463'),
					4, 'These are the perfect beginner''s skis. They''re durable without being too heavy. They slide well and you don''t have to worry about waxing (time and money saving). They''re good for several different skiing situations.',
					'11/27/2017 8:52:43 AM', '11/27/2017 4:52:43 PM');

			INSERT INTO dbo.Descriptions(DisplayIndex, StyleId, DescText)
				VALUES 
					(0, 1, 'Race skis developed for competitive athletes and expert skiers. They use the same '
						+ 'construction and pairing process as the X-ium World Cup Series, have a large sweet spot, '
						+ 'and deliver a new level of racing performance. The Rossignol Fit System measures the '
						+ 'closing flex and camber shape and ensures precisely matched pairs for optimum selection '
						+ 'and fit.'),
					(1, 1, 'Active Cap construction.'),
					(2, 1, 'Nomex honeycomb core is made of lightweight paper in a hexagonal shape to deliver a good '
						+ 'strength-to-weight ratio.'),
					(3, 1, 'Control edge: ABS sidewalls enhance torsional stability and increase power transmission '
						+ 'for more powerful kick and push-off.'),
					(4, 1, 'Double base grooves aid tracking and stability during for good directional control.'),
					(5, 1, 'Cobra Racing Sidecut widens at the forebody for superior stability at push-off and '
						+ 'enhanced fluidity when shifting weight to the new ski.'),
					(6, 1, 'K7000 universal base tuning.'),
					(7, 1, 'NIS compatible.'),
					(0, 2, 'Looking for that sweet spot between fun and stability? Salomon X-Pro with '
						+ 'pre-mounted XT12 bindings get you there. Powerline active dampening keeps the '
						+ 'feel smooth underfoot even when the terrain is mixed. Full sandwich sidewall '
						+ 'construction with ABS sidewalls provides great ski-to-snow contact while the wood '
						+ 'core delivers rebound while helping filter additional shake. Carve rocker has a '
						+ 'slight early rise tip and flat tail for easy carving and effortless turning, even at '
						+ 'high speeds. 3D race frame adds flex and a reinforced single Ti laminate layer '
						+ 'provides strong edge grip so you can bounce around the hill at will.'),
					(1, 2, 'Pre-mounted XT12 X-Track bindings (just have a ski tech adjust them to your boot and '
						+ 'preferred DIN.'),
					(2, 2, 'Spheric top sheet.'),
					(3, 2, 'Full sandwich sidewall construction, full length ABS sidewalls with laminate ' 
						+ 'construction for smooth ski to snow contact.'),
					(4, 2, 'Woodcore provides stability and rebound while filtering vibrations.'),
					(5, 2, 'Powerline magnesium active dampening system reduces vibrations and improves ski '
						+ 'to snow contact.'),
					(6, 2, 'Carve rocker has a slight early rise tip and a flat tail for easy carving and precise '
						+ 'cornering, even at high speeds.'), 
					(7, 2, '3D race frame has double fibre construction and overshaped arms for more flex performance.'),
					(8, 2, 'Reinforced single Ti laminate layer gives a great edge grip, efficient energy transfer ' 
						+ 'and liveliness.'),
					(0, 3, 'The corduroy is calling – answer boldly. These sticks by Salomon provide the ultimate ' 
						+ 'carving sensation on first tracks and fun rebound for bumping around in some loose ' 
						+ 'laying snow or ripping off moguls. Added value comes in pre-mounted XT10 Ti W X-Track '
						+ 'bindings (just have a ski tech adjust them to your boot sole and preferred DIN). ' 
						+ 'Powerline magnesium active dampening system greatly reduces chatter and gives a great '
						+ 'ski-to-snow feel. A 3D race frame provides flexibility while a single Ti laminate layer '
						+ 'adds reinforcement to grippy edges; definite perks for performance-minded skiers who '
						+ 'love to ski on-piste.'),
					(1, 3, 'Women’s ski with pre-mounted XT10 Ti W X-Track bindings.'),
					(2, 3, 'Full sandwich construction, including full length ABS sidewalls with laminate '
						+ 'construction for improved contact and absorption.'),
					(3, 3, 'Glossy top sheet.'),
					(4, 3, 'Woodcore provides stability on snow and quick rebound while smoothing ski to snow '
						+ 'contact and filtering vibrations.'),
					(5, 3, 'Powerline magnesium active dampening system substantially diminishes vibrations.'),
					(6, 3, 'Carve rocker has a slight early rise tip and a flat tail for an instantaneous carving ' 
						+ 'ensation with easy and precise cornering and edge transfer.'),
					(7, 3, '3D race frame has double fibre construction with overshaped arms for added flex performance.'),
					(8, 3, 'Single Ti laminate layer provides a great edge grip and efficient energy transfer.'),
					(9, 3, 'Great for groomed runs and mixed terrain.'),
					(0, 4, 'Forgiving skis witih just the right amount of rocker in the tips to make steering ' 
						+ 'and turning a breeze. RTM stands for Ride the Mountain, and Volkl’s collection of '
						+ 'frontside skis are made to rip resort runs. The 78 adds steel to its sandwich '
						+ 'construction, making it damp and stiff while the dual wood core keeps things slightly ' 
						+ 'flexed. Included 4Motion XL bindings give you great power transfer thanks to a wide '
						+ 'footprint.'), 
					(1, 4, 'Dual Woodcore provides progressive flex.'),
					(2, 4, 'Single sheet of steel increase dampness and stiffness.'), 
					(3, 4, 'Sidewall construction offers great durability.'),
					(4, 4, '4Motion XL bindings included have a wide foodprint for optimal power transfer.'),
					(5, 4, 'Ptex 2100 is durable and offers good wax absorption.'),
					(0, 5, 'Shiny toys for shooting the steeps. Head whipped up a cambered profile with ' 
						+ 'impressively responsive construction – Graphene, Titanal, a full wood core and '
						+ 'semi-cap construction – a ride you have to ski to believe. Dedicated skiers will '
						+ 'appreciate the race-structured UHM base that will kick your speed up a few notches. '
						+ 'PRD 12 bindings are included to keep you locked in whether you’re tackling mixed '
						+ 'conditions or raiding side pockets of pow.'),
					(1, 5, 'KERS piezoelectric device stores energy and releases it at the end of each turn for '
						+ 'control and power.'),
					(2, 5, 'ERA 3.0 S blends technologies, rocker, and shape to create a fun, fast ski.'),
					(3, 5, 'Graphene and Titanal are coming together to create a very agile and responsive ski.'),
					(4, 5, 'Full wood core adds some pop to the stability provided by the Titanal.'),
					(5, 5, 'Semi-cap construction combines the durability of a sidewall ski with the ability to '
						+ 'transfer power of a cap constructed ski.'),
					(6, 5, 'Camber underfoot with 10% rocker in the tip is ideal for hard charging on hardpack.'),
					(7, 5, 'Race structured UHM C base is the fastest base and structure available.'),
					(8, 5, 'Includes PRD 12 bindings are ideal for carving and offers DIN values of 3.5-12 and '
						+ 'a stack height of 33.5mm.'),
					(0, 6, 'Grip and rip in slalom carvers from Volkl. If you like tight edge-to-edge turns and ' 
						+ 'a sporty feel, you’ll appreciate the light but stiff construction here. A layer of '
						+ 'steel adds damper and torsional stiffness while the lightweight wood core keeps the '
						+ 'overall weight low. Advanced skiers will love ripping down groomers on this pair.'),
					(1, 6, 'Light wood core keeps the swing weight low and makes the ski more manoeuvrable.'),
					(2, 6, 'Single sheet of steel increase dampness and stiffness.'),
					(3, 6, 'Sidewall construction offers great durability.'),
					(4, 6, 'xMotion light bindings included matches the ski performance.'),
					(0, 7, 'Designed to make skiing fun and easy, so little kids take to the sport from the ' 
						+ 'get-go. The short and wide shape helps them balance and builds confidence. A ' 
						+ 'gentle sidecut gives them nice glide for a bigger woohoo factor too.'),
					(1, 7, 'Durable bindings secure with buckles and are designed to fit most winter boots.'),
					(2, 7, 'Waxless bases provide some grip on uphills, but try duct-taping a short (15-20cm) piece ' 
						+ 'of climbing skin under the binding to enhance climbing and braking performance ' 
						+ 'in hillier terrain.'), 
					(3, 7, 'Recommended for kids up to 25kg.'),
					(0, 8, 'The svelte Empire is happy maching big lines in open terrain or whistling Dixie through '
						+ 'the trees and sweet pillow drops. With a full rocker profile, this ski is fun and playful '
						+ 'in the powder, while Titanal metal sheets deliver power to hold a strong edge when railing '
						+ 'on hardpack. Climb high, plunge down deep and bring sweetness to the whole mountain.'),
					(1, 8, 'Carbon PowerRide semi-cap construction with triaxial stitched carbon fiber layer for '
						+ 'torsion and flex control.'),
					(2, 8, 'Alternating strips of paulownia and poplar wood core is light yet strong and sandwiched '
						+ 'between layers of 7000 series Titanal aluminum for beefy hard-snow performance.'),
					(3, 8, 'ABS sidewall construction for durability and performance.'),
					(4, 8, 'Rockwell C48 steel edges for sharpness and durability.'),
					(5, 8, 'Full thickness P-Tex 2000 Electra base.'),
					(6, 8, 'ull rocker is matched to the ski''s sidecut: rides an edge on hardpack and sweetly '
						+ 'floats a big soft powder arc.'),
					(0, 9, 'For skiers who get out there, way out there. Built to handle any and all backcountry '
						+ 'conditions, this is an all-around ski for long backcountry forays. Narrow and light, '
						+ 'this ski performs well on hard technical snow but is still wide and solid enough for '
						+ 'smooth descents in mixed conditions. All Terrain Rocker with early rise tip and camber '
						+ 'underfoot provides versatility and easy turning performance for skiers of all abilities, '
						+ 'in any snow condition. A good choice for all around backcountry skiing and hut tours.'),
					(1, 9, 'Torsion box cap construction and aspen, paulownia, bamboo core reduces weight.'),
					(2, 9, 'Carbon web, X-shaped carbon supports increase torsional rigidity for edge hold and grip.'),
					(3, 9, 'All terrain rocker with 30% tip rocker, 70% camber underfoot for hard and soft snow '
						+ 'performance.'),
					(4, 9, 'Progressive sidecut provides easy turning and handling for skiers of any ability.'),
					(5, 9, 'Snophobic topsheet reduces snow build up and fatigue while breaking trail.'),
					(6, 9, 'Tip and tail skin grommets can also be used to build a rescue sled.'),
					(7, 9, 'Tapered tails have a flat profile that makes them easy to plunge if needed.'),
					(0, 10, 'Stacked with features, like your favourite multi-tool. Built for dedicated backcountry '
						+ 'skiers, the Wailer Tour1 is maxed out on versatility, so you can float, grip and climb '
						+ 'whenever. Carbon laminate is incredibly light with moderate damping, power and grip. '
						+ 'Balsa wood core gives you stability and float while keeping your turns playful. Tapered '
						+ 'Paddle tech sidecut with flatter sections adds smoothness, and a World Cup Race base sends '
						+ 'you fast and hard so you can outperform your friends.'),
					(1, 10, 'our1 construction is a backcountry build that emphasizes fast ascents and class-leading '
						+ 'downhill performance. Carbon laminate is exceptionally light with moderate damping, power '
						+ 'and edge hold.'), 
					(2, 10, 'Balsa wood core combined with a larger-than-ever sweet spot results in a ski that''s '
						+ 'stable, floaty and lively in the turns.'),
					(3, 10, 'Full cap construction with a textured nylon top sheet.'),
					(4, 10, 'Paddle tech sidecut is tapered, with flatter sections that blend at the contact points '
						+ 'to make it a smooth, smooth driver.'),
					(5, 10, 'Narrow profile Rockwell 48 steel edges provide excellent edge control without sacrificing '
						+ 'a playful, floaty ride.'),
					(6, 10, 'World Cup Race base is fast and hard for exceptional performance.'),
					(0, 11, 'Every pair of Foon Skis is handcrafted in Mount Currie, BC and is milled from a single '
						+ 'piece of Coast Mountain yellow cedar. This tight-grained wood is unique in the way it '
						+ 'absorbs vibration to provide a dampened, stable ride. Unlike most wood core skis, Foon '
						+ 'uses a kiln-dried, solid 2in. board rather than layers of glued strips. It allows the '
						+ 'natural flex of the wood to transmit to the skier. Like a custom surfboard, Foon skis '
						+ 'are hand-shaped and hand-built to optimize the sidecut, base profile and flex profile. '
						+ 'The Gretski is tuned for mixed conditions, alpine tours and ski mountaineering in '
						+ 'locations where you''re 99% sure that you''re on a line that''s never been tried.'), 
					(1, 11, 'Yellow cedar core, made from a single board that''s split in two to make a pair of '
						+ 'identical cores. Yellow cedar grows only in the Pacific northwest, so if that''s where '
						+ 'you ride, ride with a slice of the mountain.'),
					(2, 11, 'Sandwich construction layered with ultralight carbon.'),
					(3, 11, 'Bevel on the topsheet makes it more durable and better at shrugging off hits from '
						+ 'the side.'),
					(4, 11, 'Active Rocker Technology creates a variable rocker that allows your skis to ride long '
						+ 'and stable and short and nimble when the turns get tight, and to smooth the way through '
						+ 'transitions. Smear, carve and smarve.'),
					(5, 11, 'Updated 5-point sidecut gives the skis a powerful edge that stics to the steeps.'),
					(6, 11, 'Early rise tip for variable snow conditions.'),
					(0, 12, 'Get after some all-mountain madness on a ski that''s ready for fun. Head LIBRA technology '
						+ 'sets you up with a sweet combo of control, balance and lightness. Graphene construction '
						+ 'boosts performance, Koroyd and carbon add dampness and torsional stiffness, and the 80/20 '
						+ 'camber-rocker combo adds some bounce. At 85mm underfoot, the Total Joy Skis are ready for '
						+ 'big on-piste performance – and they come with bindings in the box so you can get out there '
						+ 'ASAP.'),
					(1, 12, 'ERA 3.0 combines technology, rocker and shape for optimal performance.'),
					(2, 12, 'Graphene layer adds strength and very little weight.'),
					(3, 12, 'Construction combines the stiffness of carbon with the dampness of Koyord.'),
					(4, 12, 'Wood core makes them stable and responsive.'),
					(5, 12, 'Camber underfoot with 20% rocker in the tip is tuned for female shredders.'),
					(6, 12, 'Structured UHM C base is fast enough for race skis.'),
					(7, 12, 'Included JOY 11 GW SLR bindings are GripWalk comptible and have DIN values from 3-11.'),
					(0, 13, 'The widest of the Volkl Big Mountain Touring line, the V-Werks BMT 122 is bouncy, '
						+ 'buoyant and engineered for unforgettable descents. The early rise taper is progressive and '
						+ 'keeps the ski lightweight for way-out-there lines that belong only to you. Full rocker '
						+ 'provides fun-filled descents in all conditions. Float through deep pillows and surf '
						+ 'perfect powder bowls with this big mountain rig.'), 
					(1, 13, 'Progressive tapered shape with a full carbon jacket.'), 
					(2, 13, 'Poplar wood core with dense ash wood in the binding area for secure screw retention '
						+ 'and stability. The softer core provides snappy resilience and flex pattern tailored to length.'),
					(3, 13, 'Full rocker provides fun filled descents in all conditions.'), 
					(4, 13, 'Minimalist skin clamp system uses a simple pin in the ski tip.'),
					(5, 13, 'Volkl recommends using Marker Bindings with these skis. Please consult with a ski tech '
						+ 'to use a non-Marker Binding, MEC ski techs have templates from Volkl that will determine '
						+ 'the best mounting configuration.'),
					(0, 14, 'Let''s get to it. The Vapor Svelte is made to bust through crud, lay down arcs on the '
						+ 'hardpack and float through waist deep powder. The Kevlar core and Carbon Nano Tube '
						+ 'technology gives it power transfer to take on wind-blown slopes and hateful crust, while '
						+ 'an early rise tip and tail offer float when you''re out tracking champagne pow. Going up, '
						+ 'these sticks are quick and snappy, and at 40% lighter than other (wood core) mid-fats, '
						+ 'they are sure to have a place in your heart and in your quiver for many seasons to come.'), 
					(1, 14, 'Carbon Nano Tube construction keeps the weight low and the stiffness high.'),
					(2, 14, 'Kevlar weave composite core along with carbon fiber reinforcement makes for an extremely '
						+ 'light ski that''s big-line ready.'),
					(3, 14, 'Nylon topsheet is durable and won''t hold snow.'),
					(4, 14, 'The 96mm waist is great for all-around ski mountaineering.'),
					(5, 14, 'Early rise in the tip and tail allows floats easily in soft snow, while the camber '
						+ 'underfoot ensure a solid grip on hardpack.'),
					(6, 14, 'P-Tex 5000 provides great glide and absorbs wax very well.'),
					(7, 14, '2.2mm steel edges will offer years of service.'),
					(0, 15, 'Grown in Utah, the Super Chargers are a stiffer, more aggressive version of the Voilé '
						+ 'Charger Skis. With a wood core, carbon and triaxial fiberglass layers, they feel light '
						+ 'and remain very playful dipping into the trees or arcing through wide open bowls. The '
						+ 'super versatile 102mm waist opens up the whole mountain and takes you through a full '
						+ 'season of snow conditions. All great stuff, and tuned for women.'), 
					(1, 15, 'Lightweight aspen wood core shaves weight while providing liveliness.'),
					(2, 15, 'Carbon fiberglass layer increase liveliness and strength but keep the weight low.'),
					(3, 15, 'Triaxial fiberglass layer makes for a responsive and torsionally stiff ski.'),
					(4, 15, 'Nylon topsheet is durable and prevents snow from sticking to the ski.'),
					(5, 15, '2mm, full-perimeter steel edges allows for seasons of tuning.'),
					(6, 15, 'Early rise in the tip and tail improves float when the snow density is low and the stoke '
						+ 'is high.'),
					(0, 16, 'With the same versatile platform as the iconic 112RP2 series, the 99 blends downhill '
						+ 'performance with the speed and lightness of a full-fledged touring ski. When slicing corners '
						+ 'in hardpack, the rocker and variable sidecut provide outstanding edge control and power. '
						+ 'In soft or mixed snow, you can draw on the gradually rockered paddle tech shape for float and '
						+ 'smooth arcing out. For alpine tours with high-intensity descents, the on-edge, off-edge '
						+ 'performance lets you get creative and ski through whatever soft, crusty or weirdly mixed '
						+ 'conditions you come across.'),
					(1, 16, 'Tour1 construction is a backcountry build that '
						+ 'emphasizes fast ascents and class-leading '
						+ 'downhill performance. Carbon laminate is '
						+ 'exceptionally light with moderate damping, '
						+ 'power and edge hold.'),
					(2, 16, 'Balsa wood core combined with a larger-than-ever sweet spot results in ' 
						+ 'a ski that''s stable, floaty and lively in the turns.'),
					(3, 16, 'Full cap construction with a textured nylon top sheet.'),
					(4, 16, 'Paddle tech sidecut is tapered, with flatter sections that blend at the ' 
						+ 'contact points to make it a smooth driver.'),
					(5, 16, 'Narrow profile Rockwell 48 steel edges provide excellent edge control without ' 
						+ 'sacrificing a playful, floaty ride.'),
					(6, 16, 'World Cup Race base is fast and hard for exceptional performance.'),
					(0, 17, 'Wide-bodied, versatile skis with a stiff camber that''s easy to handle. Great glide for '
						+ 'exploring on and off-track and all-day excursions.'),
					(1, 17, 'Densolite 1000 shaped foam and fibreglass core.'),
					(2, 17, 'D construction for more consistent, predictable behavior.'),
					(3, 17, 'Light sidecut puts the narrowest point toward the binding, for extra control and '
					+ 'stability, especially when descending out of the track.'),
					(4, 17, 'Low profile tip and tail are light to improve your kick and swing.'),
					(5, 17, 'Camber gives complete contact of the grip zone with the snow during kick for '
					+ 'more maneuverability.'),
					(6, 17, 'G2 Plus Grip waxless bases with Salomon® performance universal grinding base tuning.'),
					(7, 17, 'Tail protectors.'), 
					(8, 17, 'Lengths: 163 cm (Small), 173 cm (Medium), 183 cm (Large)'), 
					(0, 18, 'Lightweight skate skis for general purpose training, fitness, and loppett racing. '
						+ 'The wood core keeps the grams down and delivers the performance that Fischer is known '
						+ 'for. The ultra-finish technology removes a thin layer of base prior to the final grind '
						+ 'for a flat, race-ready finish.'),
					(1, 18, 'Air Tec fuma wood cores with air channeling.'),
					(2, 18, 'Skatecut features a javelin type skating sidecut.'),
					(3, 18, 'WC Pro high density universal sintered bases.'),
					(4, 18, 'NIS mounting plates included, bindings sold separately.'),
					(0, 19, 'Designed for power and smooth, swift skating. The Equipe 8 are a good choice for '
						+ 'training and racing or for performance focused skiers looking to for a fast, stable ski.'),
					(1, 19, 'Densolite 3000 core uses foam and wood with tri-directional fibreglass laminate '
						+ 'that''s light and responsive.'),
					(2, 19, 'Javelin skating sidecut has a narrow profile, designed for speed.'),
					(3, 19, 'Low heel-toe camber is efficient and puts power into your stride.'),
					(4, 19, 'G4 Zeolit base, the Zeolit additive provides good wax absorption and retention. '
						+ 'Protective foil guards the base from oxidation, UV and scratches.'),
					(5, 19, 'Universal Race grinding uses low-pressure stone grinding to make these fast in all '
						+ 'conditions.'),
					(0, 20, 'Super, all-round skis that suit performance training and racing. The channelled air '
						+ 'cores are light and maintain an ideal flex profile at any temperature.'),
					(1, 20, 'Air Core Basalight construction has air channels in a lightweight wood core, and '
						+ 'light basalt fibres to keep the weight down.'),
					(2, 20, 'Skate 115 construction places camber and snow contact points further apart for increased '
						+ 'stability and a more powerful kick action on hard snow.'),
					(3, 20, 'Power edges are designed to strengthen the edge of the ski, protect against wear on the '
						+ 'sidewalls, and promote even wax wear.'),
					(4, 20, 'DTG World Cup Pro Base is a sintered base with 7.5% graphite content to suit universal '
						+ 'temperature range.'),
					(5, 20, 'Base tuning uses NATURAL diamond FOR an ultra-smooth finish.'),
					(6, 20, 'NIS plate system. Bindings available separately.'),
					(0, 21, 'Train or race with these high-performance skis. They deliver precision and power to '
						+ 'delight experts and competitive types. A specialized kick zone provides excellent grip '
						+ 'and stability that efficiently transfers power and momentum.'),
					(1, 21, 'Active cap construction keeps your ride lively, floaty, and directionally stable.'),
					(2, 21, 'Extended edges.'),
					(3, 21, 'Cobra sidecut provides stability on push-off.'),
					(4, 21, 'Honeycomb cores deliver strength without excess weight.'),
					(5, 21, 'K7000 racing base with double grooves to help keep you straight in your tracks.'),
					(6, 21, 'NIS system.'),
					(0, 22, 'An admirable touring set-up for exploring the backcountry without packing a lot of '
						+ 'weight. The ski camber is fine-tuned for a graceful glide even on rough and varied snow. '
						+ 'Steel edges give you fantastic control on the downhill.'), 
					(1, 22, 'Nordic rocker camber has slightly opened tips, for effortless turning and a long gliding '
						+ 'phase over ungroomed terrain.'),
					(2, 22, 'Speed Grinding base with universal stone grinding for unrestricted gliding in all snow '
						+ 'conditions.'),
					(3, 22, 'Skin attachment system (skins available separately) clicks in place at tip, passes '
						+ 'through a notch in the middle of the skin and attaches at deck.'),
					(4, 22, 'Skins can be applied to prepared skis (waxed) bases.'),
					(5, 22, 'Binding not included on this model.'),
					(0, 23, 'Great for athletic skiers who want to go fast. These are classic skis with a twist, '
						+ 'instead of fishscale waxless base, they have Atomic skintec technolgoy: a snap-in mohair '
						+ 'skin insert provides great grip on the hills, excellent kick and exceptional glide.'),
					(1, 23, 'Cap construction with step down sidewalls for better steering and power transmission.'),
					(2, 23, 'Densolite core is lightweight'),
					(3, 23, 'Efficient Skintec technology works in all snow conditions and temperatures. Mohair grip '
						+ 'zones are replaceable after heavy wear thanks to snap in technology.'),
					(4, 23, '5000 bases'), 
					(0, 24, 'A stiffened up version of the Neo, the Superneo has the same sidecut and rocker profile '
						+ 'but uses carbon fiber/kevlar to add stiffness, ability to transfer power, and readiness to '
						+ 'charge. Every pair of Foon Skis is handcrafted in Mount Currie, BC and is milled from a '
						+ 'single piece of Coast Mountain yellow cedar. This tight-grained wood is unique in the way '
						+ 'it absorbs vibration to provide a daaamp, stable ride. Unlike most wood core skis, Foon '
						+ 'uses a kiln-dried, solid 2in. board rather than layers of glued strips. It allows the '
						+ 'natural flex of the wood to transmit to the skier. Like a custom surfboard, Foon skis are '
						+ 'hand-shaped and hand-built to optimize the sidecut, base profile and flex profile.'),
					(1, 24, 'Sandwich construction blends carbon fiber, kevlar, yellow cedar with a Durasurf 4001 '
						+ 'graphite black base.'),
					(2, 24, 'Yellow cedar core, made from a single board that''s split in two to make a pair of '
						+ 'identical cores. Yellow cedar grows only in the Pacific northwest, so if that''s where '
						+ 'you ride, ride with a slice of the mountain.'),
					(3, 24, 'Core profile is shaped so the power and balance point is at the ball of the foot to '
						+ 'maximize feel and control.'),
					(4, 24, 'Active Rocker Technology creates a variable rocker that allows your skis to ride long '
						+ 'and stable and short and nimble when the turns get tight, and to smooth the way through '
						+ 'transitions.'),
					(5, 24, 'Early rise tip and tail with a short, but pronounced camber for a vibrant, lively ride.'), 
					(0, 25, 'Charge the slopes with confidence. Head’s Strong Instinct Ti skis adapt to whatever '
						+ 'conditions are thrown at them – hardpack, slow snow, fast groomers or boilerplate, you '
						+ 'name it, these skis will rip it. Graphene and Titanal keep the construction light but '
						+ 'precise and the Allride 80% camber and 20% rocker combo keep you grounded to carve '
						+ 'corduroy but float powder laps while you can. ABS sidewalls cut through chatter and '
						+ 'included PR 11 bindings keep you locked in.'),
					(1, 25, 'ERA 3.0 combines technology, rocker, and shape for optimal performance.'),
					(2, 25, 'A layer of Graphene adds a lot of strength and very little weight.'),
					(3, 25, 'Durable ABS sidewalls with a layer of Titanal makes for a resistant and torsionally '
						+ 'stiff ski.'),
					(4, 25, 'Camber underfoot with 20% rocker in the tip is ideal for all-mountain shredders.'),
					(5, 25, 'Structured UHM C base is race ski fast.'),
					(6, 25, 'Included PR 11 bindings offers DIN value of 3-11 and a stand height of 31mm.');

			INSERT INTO dbo.SpecKeys ([SpecKeyId], [SpecKeyName], [SpecKeyDesc]) 
				VALUES (1, 'Ideal for', NULL), (2, 'Weight', NULL), (3, 'Dimension', NULL), (4, 'Ability', 'Beginner: new or getting' 
					+ 'back into skiing. Intermediate: confident with turning and stopping. Advanced: highly ' + 
					+ 'confident carver. Expert: steeps, drops, park, double blacks -- bring it on.'), (5, 'Made in', NULL), 
					(6, 'Construction', 'Sandwich and combination construction is generally used for big ' 
					+ 'freeride-style skis while the lighter structural cap type of construction is more'
					+ 'commonly found on cross-country skis.'), (7, 'core', NULL), (8, 'Bindings included', NULL), 
					(9, 'Snow conditions', NULL), (10, 'Terrain', NULL), (11, 'Rocker/camber profile', NULL), 
					(12, 'Turning radius', NULL), (13, 'User weight range', 'A good starting point for selecting ski ' 
					+ 'or snowboard length. You might choose a shorter or longer length based on terrain and '
					+ 'ability.'), (14, 'Metal edge', NULL), (15, 'Waxless', 'Often called "fish scale" skis, ' 
					+ 'waxless skis are designed so you don''t need to apply kick wax. However, you may still '
					+ 'want to apply glide wax for a better glide. They are a good option for those who don''t ' 
					+ 'like waxing, or for regions where temperatures can vary greatly throughout the day.'); 

			INSERT INTO dbo.SpecCores(CoreId, CoreSpec)
				VALUES (1, 'Synthetic'), (2, 'Wood'), (3, 'Densolite'), (4, 'Wood/Carbon'), (5, 'Synthetic/Wood'), 
					(6, 'Wood/Air channel'), (7, 'Air Core Basalight'), (8, 'Synthetic honeycomb');

			INSERT INTO dbo.SpecConstructions(ConstructionId, ConstructionSpec)
				VALUES (1, 'Extruded plastic'), (2, 'Structural cap'), (3, 'Sandwich'), (4, 'Combination');

			INSERT INTO dbo.SpecAbilities(AbilityId, AbilitySpec)
				VALUES (1, 'Beginner'), (2, 'Intermediate'), (3, 'Advanced'), (4, 'Expert');

			INSERT INTO dbo.SpecSnowConditions(SnowConditionId, SnowConditionSpec)
				VALUES (1, 'Mixed'), (2, 'Powder'), (3, 'Groomed');

			INSERT INTO dbo.SpecTerrains(TerrainId, TerrainSpec)
				VALUES (1, 'Off-Piste'), (2, 'On-Piste');

			INSERT INTO dbo.SpecRockerCamberProfiles (RockerCamberProfileId, RockerCamberProfileSpec) 
				VALUES (1, 'Rocker tip'), (2, 'Rocker tail'), (3, 'Full rocker');

			INSERT INTO dbo.SpecTextValues(StyleId, DisplayIndex, SpecKeyId, TextValue)
				VALUES (1, 4, 3, '40-44-43-43mm'), (6, 1, 2, '5.5kg (163cm)'), (6, 9, 3, '122-73-103mm (163cm)'), 
					(6, 10, 12, '14.3m (163cm)'), (7, 1, 2, '920g (60cm)'), (7, 6, 3, '58-55-65mm (Spike 60cm)'),
					(8, 8, 3, '145-115-126mm'), (8, 9, 12, '17m'), (9, 6, 3, '128-96-118mm'), (9, 7, 12, '17m'), 
					(10, 1, 2, '2.89kg (178cm)'), (10, 9, 3, '133-106-122mm (178cm)'), (10, 10, 12, '18m (178cm)'),
					(12, 1, 2, '4.54kg (148cm)'), (12, 9, 3, '131-84-111mm (148cm), 132-84-112mm (153cm), '
					+ '132-85-112mm (158cm), 133-85-133mm (163cm), 134-85-114mm (168cm)'), (12, 10, 12, '1m (148cm), '
					+ '11.8m (153cm), 12.7m (158cm), 13.6m (163cm), 14.5m (168cm)'), (13, 6, 3, '143-122-133mm'), 
					(13, 7, 12, '28.2m'), (16, 1, 2, '2.82kg (184cm)'), (16, 9, 3, '125-99-111mm (184cm)'),
					(16, 10, 12, '18m (184cm)'), (17, 1, 2, '1.25kg (Medium)'), (17, 5, 3, '59-55-51-55mm'),
					(17, 6, 13, '40-50 kg (Small), 50-65kg (Medium), 65-80kg (Large)'), (18, 4, 3, '41-44-44mm'), 
					(19, 4, 3, '42-44-43-45mm'), (20, 4, 3, '41-44-44mm'), (21, 4, 3, '44-44-44mm'), (22, 2, 2, 
					'1.95kg (200cm)'), (22, 5, 3, '66-54-61mm (195cm)'), (22, 6, 13, '0-55kg (180cm), 50-59kg '
					+ '(185cm), 55-64kg (190cm), 60-74kg (195cm), 65-84kg (200cm), 70-89kg (205cm), 80-90+kg (210cm)'),
					(23, 4, 3, '43-44-43mm');

			INSERT INTO dbo.SpecBitValues(StyleId, DisplayIndex, SpecKeyId, [SpecValue])
				VALUES (1, 5, 15, 0), (1, 6, 14, 0), (1, 7, 8, 0), (2, 6, 8, 1), (3, 6, 8, 1), (4, 8, 8, 1), 
					(5, 8, 8, 1), (6, 11, 8, 1), (7, 7, 8, 1), (8, 10, 8, 1), (9, 8, 8, 0), (10, 11, 8, 0), 
					(11, 8, 8, 0), (12, 11, 8, 1), (13, 8, 8, 0), (14, 8, 8, 0), (15, 8, 8, 0), (16, 11, 8, 0), 
					(17, 7, 15, 1), (17, 8, 14, 0), (17, 9, 8, 0), (18, 5, 15, 0), (18, 6, 14, 0), (18, 7, 8, 0),
					(19, 5, 15, 0), (19, 6, 14, 0), (19, 7, 8, 0), (20, 5, 15, 0), (20, 6, 14, 0), (20, 7, 8, 0), 
					(21, 5, 15, 0), (21, 6, 14, 0), (21, 7, 8, 0), (22, 7, 15, 0), (22, 8, 14, 1), (22, 9, 8, 0), 
					(23, 5, 15, 1), (23, 6, 14, 0), (23, 7, 8, 0), (24, 8, 8, 0), (25, 8, 8, 1);

			INSERT INTO dbo.SpecSingleValues(StyleId, DisplayIndex_Core, CoreId, DisplayIndex_Construction, 
				ConstructionId, DisplayIndex_MadeIn, MadeInId)
				VALUES (1, 3, 1, 2, 2, 8, 8) , (2, 4, 2, 3, 3, 7, 6), (3, 4, 2, 3, 3, 7, 6), (4, 6, 2, 5, 3, 9, 4),
					(5, 6, 2, 5, 4, 9, 3), (6, 7, 2, 6, 3, 12, 4), (7, 5, 1, 4, 1, 8, 3), (8, 6, 2, 5, 3, 11, 7), 
					(9, 4, 2, 3, 2, 9, 7), (10, 7, 2, 6, 2, 12, 1), (11, 6, 4, 5, 3, 9, 2), (12, 7, 5, 6, 4, 12, 6),
					(13, 4, 2, 3, 3, 9, 4), (14, 6, 1, 5, 2, 9, 1), (15, 6, 2, 5, 2, 9, 1), (16, 7, 2, 6, 2, 12, 1), 
					(17, 4, 3, 3, 2, 10, 9), (18, 3, 6, 2, 2, 8, 5), (19, 3, 3, 2, 2, 8, 6), (20, 3, 7, 2, 2, 8, 6), 
					(21, 3, 8, 2, 2, 8, 6), (22, 4, 6, 3, 2, 10, 5), (23, 3, 3, 2, 4, 8, 6), (24, 6, 2, 5, 3, 9, 2), 
					(25, 6, 1, 5, 3, 9, 3); 

			INSERT INTO dbo.SpecMultiValues(StyleId, DisplayIndex_IdealFor, DisplayIndex_Ability, 
				DisplayIndex_SnowCondition, DisplayIndex_Terrain, DisplayIndex_RockerCamberProfile)
				VALUES (1, 1, NULL, NULL, NULL, NULL), (2, 1, NULL, 2, NULL, 5), (3, 1, NULL, 2, NULL, 5), 
					(4, 1, 4, 2, 3, 7), (5, 1, 4, 2, 3, 7), (6, 2, 5, 3, 4, 8), (7, 2, 3, NULL, NULL, NULL), 
					(8, 1, 4, 2, 3, 7), (9, 1, NULL, 2, NULL, 5), (10, 2, 5, 3, 4, 8), (11, 1, 4, 2, 3, 7), 
					(12, 2, 5, 3, 4, 8), (13, 1, NULL, 2, NULL, 5), (14, 1, 4, 2, 3, 7), (15, 1, 4, 2, 3, 7), 
					(16, 2, 5, 3, 4, 8), (17, 2, NULL, NULL, NULL, NULL), (18, 1, NULL, NULL, NULL, NULL), 
					(19, 1, NULL, NULL, NULL, NULL), (20, 1, NULL, NULL, NULL, NULL), (21, 1, NULL, NULL, NULL, NULL), 
					(22, 1, NULL, NULL, NULL, NULL), (23, 1, NULL, NULL, NULL, NULL), (24, 1, 4, 2, 3, 7), 
					(25, 1, 4, 2, 3, 7);
                    
			INSERT INTO dbo.StyleAbilities(StyleId, AbilityId)
				VALUES (7, 1), (4, 1), (4, 2), (4, 3), (5, 3), (5, 4), (6, 1), (6, 2), (6, 3), (8, 3), (8, 4), (10, 2),
					(10, 3), (10, 4), (11, 3), (11, 4), (12, 1), (12, 2), (12, 3), (12, 4), (14, 2), (14, 3), (14, 4), 
					(15, 2), (15, 3), (15, 4), (16, 2), (16, 3), (16, 4), (24, 3), (24, 4), (25, 1), (25, 2), (25, 3);

			INSERT INTO dbo.StyleSnowConditions (StyleId, SnowConditionId)
				VALUES (2, 3), (2, 1), (3, 3), (3, 1), (4, 3), (5, 1), (5, 3), (6, 3), (8, 2), (8, 1), (9, 1), (9, 2), 
					(10, 2), (11, 1), (12, 3), (12, 1), (13, 2), (13, 1), (14, 2), (14, 1), (15, 2), (15, 1), (16, 1), 
					(16, 2), (24, 1), (24, 2), (25, 1), (25, 2), (25, 3);

			INSERT INTO dbo.StyleTerrains(StyleId, TerrainId)
				VALUES (4, 1), (5, 2), (6, 2), (8, 1), (10, 1), (11, 1), (12, 1), (12, 2), (14, 1), (15, 2), (15, 1), 
					(16, 1), (24, 2), (24, 1), (25, 1), (25, 2);

			INSERT INTO dbo.StyleRockerCamberProfiles (StyleId, RockerCamberProfileId)
				VALUES (2, 1), (3, 1), (4, 1), (5, 1),(6, 1), (8, 3), (9, 1), (9, 2), (10, 1), (10, 2), (11, 1), 
					(12, 1), (13, 3), (14, 1), (14, 2), (15, 1), (15, 2), (16, 1), (16, 2), (24, 1), (24, 2), (25, 1);

            
	END

DECLARE @dbName AS NVARCHAR(50) = (SELECT DB_NAME());
DECLARE @dbProd AS NVARCHAR(50) = 'SkiShop';

IF @dbName = @dbProd
	ALTER DATABASE SkiShop 
	SET READ_COMMITTED_SNAPSHOT ON WITH ROLLBACK IMMEDIATE




    



