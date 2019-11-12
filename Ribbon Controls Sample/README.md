# Ribbon Controls Sample
## Requires
- Visual Studio 2010
## License
- Custom
## Technologies
- Office
- VSTO
- Microsoft Office Excel 2007
## Topics
- Controls
- Office Ribbon
## Updated
- 03/20/2012
## Description

<table cellspacing="0" cellpadding="0" width="100%" style="text-align:left; margin-top:5px; font-size:10px">
<tbody>
<tr>
<th align="left" style="text-align:left; background-color:#efeff7; color:#000066">
<strong>Remarque&nbsp;:</strong></th>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:10px; margin-bottom:5px">Cet exemple s'ex&eacute;cute dans Microsoft Office Excel&nbsp;2007 et versions ult&eacute;rieures.</p>
</td>
</tr>
</tbody>
</table>
<p><span class="Apple" style="widows:2; text-transform:none; text-indent:0px; border-collapse:separate; font:medium 'Times New Roman'; white-space:normal; orphans:2; letter-spacing:normal; color:#000000; word-spacing:0px"><span class="Apple" style="font-family:Verdana; font-size:10px">&nbsp;</span></span></p>
<div class="introduction">
<p style="margin-top:10px; margin-bottom:5px">Cet exemple explique comment cr&eacute;er un onglet personnalis&eacute; qui s'affiche sur le Ruban d'une feuille de calcul Microsoft Office Excel. Ce Ruban personnalis&eacute; montre la plupart des contr&ocirc;les
 disponibles dans le groupe<span class="ui">Contr&ocirc;les de ruban Office</span><span class="Apple">&nbsp;</span>de la<span class="Apple">&nbsp;</span><span class="ui">Bo&icirc;te &agrave; outils</span><span class="Apple">&nbsp;</span>de Visual
 Studio. Pour plus d'informations sur l'utilisation de ces contr&ocirc;les, consultez la rubrique Concepteur de ruban.</p>
</div>
<p>&nbsp;</p>
<table cellspacing="0" cellpadding="0" width="100%" style="text-align:left; margin-top:5px; font-size:10px">
<tbody>
<tr>
<th align="left" style="text-align:left; background-color:#efeff7; color:#000066">
<strong>Note de s&eacute;curit&eacute;&nbsp;:</strong></th>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:10px; margin-bottom:5px">Cet exemple de code a pour but d'illustrer un concept et affiche uniquement le code pertinent pour ce concept. Il peut ne pas r&eacute;pondre aux exigences de s&eacute;curit&eacute; d'un environnement sp&eacute;cifique
 et ne doit pas &ecirc;tre utilis&eacute; tel quel. Nous vous conseillons d'ajouter un code de gestion des erreurs et de s&eacute;curit&eacute; afin de renforcer la s&eacute;curit&eacute; et la fiabilit&eacute; de vos projets. Microsoft fournit cet exemple
 de code &laquo;&nbsp;EN L'&Eacute;TAT&nbsp;&raquo;, sans garantie d'aucune sorte.</p>
</td>
</tr>
</tbody>
</table>
<p><span class="Apple" style="widows:2; text-transform:none; text-indent:0px; border-collapse:separate; font:medium 'Times New Roman'; white-space:normal; orphans:2; letter-spacing:normal; color:#000000; word-spacing:0px"><span class="Apple" style="font-family:Verdana; font-size:10px">&nbsp;</span></span></p>
<div class="introduction">
<p style="margin-top:10px; margin-bottom:5px">Pour plus d'informations sur la fa&ccedil;on d'installer l'exemple de projet sur votre ordinateur, consultez la rubrique Comment&nbsp;: installer et utiliser des exemples de fichiers trouv&eacute;s dans l'aide.</p>
</div>
<h3 class="procedureSubHeading">Pour ex&eacute;cuter cet exemple</h3>
<div class="subSection">
<ol>
<li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Appuyez sur&nbsp;F5.</p>
</li><li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Une feuille de calcul Excel s'affiche. Le ruban de la feuille de calcul affiche un onglet personnalis&eacute; nomm&eacute;<span class="Apple">&nbsp;</span><span class="ui">Ribbon Control Sample</span>.</p>
<p style="margin-top:10px; margin-bottom:5px">Le ruban n'affiche aucun autre onglet, car la propri&eacute;t&eacute; StartFromScratch du ruban personnalis&eacute; a la valeur<span class="Apple">&nbsp;</span><span class="keyword">true</span>.</p>
</li></ol>
</div>
<h1 class="heading" style="color:#003399; font-size:12px"><span>Configuration requise</span></h1>
<div class="section" id="requirementsTitleSection">
<p style="margin-top:10px; margin-bottom:5px">Cet exemple requiert les applications suivantes&nbsp;:</p>
<ul>
<li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Visual Studio Tools pour Office.</p>
</li><li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Microsoft Office Excel&nbsp;2007.</p>
</li></ul>
</div>
<h1 class="heading" style="color:#003399; font-size:12px"><span>Fonctionnement</span></h1>
<div class="section" id="demonstratesSection">
<p style="margin-top:10px; margin-bottom:5px">Cet exemple illustre les concepts suivants&nbsp;:</p>
<ul>
<li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Personnalisation d'un onglet &agrave; l'aide d'un mod&egrave;le d'&eacute;l&eacute;ment<span class="Apple">&nbsp;</span><span class="ui">Ruban (Concepteur visuel)</span>.</p>
</li><li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Masquage de tous les onglets int&eacute;gr&eacute;s et de la plupart des commandes du menu Office et affichage des personnalisations d&eacute;finies uniquement dans cet &eacute;l&eacute;ment Ruban.</p>
</li><li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Ajout de groupes et de contr&ocirc;les personnalis&eacute;s au Concepteur de ruban.</p>
</li><li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Gestion des &eacute;v&eacute;nements de contr&ocirc;les sur le ruban.</p>
</li><li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Modification des propri&eacute;t&eacute;s de contr&ocirc;les au moment de l'ex&eacute;cution.</p>
</li><li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Ajout dynamique de contr&ocirc;les &agrave; un menu au moment de l'ex&eacute;cution.</p>
</li><li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Ajout dynamique d'&eacute;l&eacute;ments &agrave; une galerie au moment de l'ex&eacute;cution.</p>
</li><li style="margin-top:-2px; margin-bottom:3px">
<p style="margin-top:10px; margin-bottom:5px">Affichage et masquage de contr&ocirc;les du volet Actions &agrave; l'aide de boutons sur le ruban.</p>
</li></ul>
<h1 class="heading" style="color:#003399; font-size:12px"><span>Groupe Working with Sheets</span></h1>
<div class="section" id="sectionSection0">
<p style="margin-top:10px; margin-bottom:5px">Le tableau suivant d&eacute;crit les contr&ocirc;les qui s'affichent dans le groupe<span class="Apple">&nbsp;</span><span class="ui">Working with Sheets</span><span class="Apple">&nbsp;</span>du ruban personnalis&eacute;.</p>
</div>
</div>
<p>&nbsp;</p>
<table border="0" cellspacing="2" cellpadding="5" frame="lhs" style="text-align:left; margin-top:5px; font-size:10px; width:100%">
<tbody>
<tr>
<th style="text-align:left; background-color:#efeff7; color:#000066">
<p style="margin-top:1px; margin-bottom:4px">Contr&ocirc;le</p>
</th>
<th style="text-align:left; background-color:#efeff7; color:#000066">
<p style="margin-top:1px; margin-bottom:4px">Description</p>
</th>
<th style="text-align:left; background-color:#efeff7; color:#000066">
<p style="margin-top:1px; margin-bottom:4px">Action/R&eacute;sultat</p>
</th>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px"><span class="ui">Afficher le volet des actions</span></p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Bouton bascule affich&eacute; comme &eacute;tant activ&eacute; ou non activ&eacute;.</p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Cliquez sur<span class="Apple">&nbsp;</span><span class="ui">Afficher le volet des actions</span>.</p>
<p style="margin-top:1px; margin-bottom:4px">Un volet Actions s'affiche en regard de la feuille de calcul.</p>
<p style="margin-top:1px; margin-bottom:4px">Cliquez une deuxi&egrave;me fois sur<span class="Apple">&nbsp;</span><span class="ui">Afficher le volet des actions</span><span class="Apple">&nbsp;</span>pour masquer celui-ci.</p>
</td>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Boutons de face</p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Trois boutons contenus dans un groupe de boutons. Ces boutons sont ajout&eacute;s au groupe car ils sont li&eacute;s les uns aux autres. Les boutons d'un groupe de boutons ont une apparence brillante.</p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Cliquez sur un bouton de face.</p>
<p style="margin-top:1px; margin-bottom:4px">La cellule&nbsp;A1 affiche l'image correspondante.</p>
</td>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px"><span class="ui">Alignement</span></p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Bouton partag&eacute;. Un bouton partag&eacute; est un bouton auquel est associ&eacute; un menu. Le menu de bouton partag&eacute;<span class="Apple">&nbsp;</span><span class="ui">Alignement</span>contient trois
 boutons. La propri&eacute;t&eacute; OfficeImageId du bouton partag&eacute;<span class="Apple">&nbsp;</span><span class="ui">Alignment</span><span class="Apple">&nbsp;</span>a comme valeur l'ID d'un contr&ocirc;le d'alignement Office int&eacute;gr&eacute;.</p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Cliquez sur<span class="Apple">&nbsp;</span><span class="ui">Alignement &agrave; droite</span>,<span class="Apple">&nbsp;</span><span class="ui">Alignement &agrave; gauche</span>ou<span class="Apple">&nbsp;</span><span class="ui">Alignement
 au centre</span>dans le menu de bouton partag&eacute;<span class="Apple">&nbsp;</span><span class="ui">Alignement</span>.</p>
<p style="margin-top:1px; margin-bottom:4px">Le texte qui s'affiche dans la cellule&nbsp;A3 est align&eacute; &agrave; droite, align&eacute; &agrave; gauche ou centr&eacute;.</p>
</td>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px"><span class="ui">Couleur</span></p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Galerie qui pr&eacute;sente un tableau de sph&egrave;res color&eacute;es parmi lesquelles vous pouvez faire votre choix.</p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Cliquez sur<span class="Apple">&nbsp;</span><span class="ui">Couleur</span>, puis s&eacute;lectionnez une couleur de la galerie.</p>
<p style="margin-top:1px; margin-bottom:4px">Une sph&egrave;re de la couleur s&eacute;lectionn&eacute;e s'affiche dans la cellule&nbsp;A6.</p>
</td>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px"><span class="ui">Format de graphique</span></p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Contr&ocirc;le d&eacute;roulant qui contient une liste des formats de graphique. Contrairement &agrave; une zone de liste d&eacute;roulante, un contr&ocirc;le d&eacute;roulant n'accepte pas la saisie de s&eacute;lections.</p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Cliquez sur<span class="Apple">&nbsp;</span><span class="ui">Format de graphique</span>, puis s&eacute;lectionnez un format dans la liste.</p>
<p style="margin-top:1px; margin-bottom:4px">Le format du graphique qui s'affiche sur la feuille de calcul est adapt&eacute; au format s&eacute;lectionn&eacute;.</p>
</td>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px"><span class="ui">MRU Find</span></p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Zone de liste d&eacute;roulante. Vous pouvez entrer votre choix ou le s&eacute;lectionner.</p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Cliquez sur la zone de liste d&eacute;roulante<span class="Apple">&nbsp;</span><span class="ui">MRU Find</span>, puis s&eacute;lectionnez le texte dans la liste.</p>
<p style="margin-top:1px; margin-bottom:4px">-&nbsp;ou&nbsp;-</p>
<p style="margin-top:1px; margin-bottom:4px">Entrez votre texte dans la zone de liste d&eacute;roulante<span class="Apple">&nbsp;</span><span class="ui">MRU Find</span>, puis appuyez sur Entr&eacute;e.</p>
<p style="margin-top:1px; margin-bottom:4px">Un message identifiant l'emplacement du texte sur la feuille de calcul s'affiche.</p>
</td>
</tr>
</tbody>
</table>
<p><span class="Apple" style="widows:2; text-transform:none; text-indent:0px; border-collapse:separate; font:medium 'Times New Roman'; white-space:normal; orphans:2; letter-spacing:normal; color:#000000; word-spacing:0px"><span class="Apple" style="font-family:Verdana; font-size:10px">&nbsp;</span></span></p>
<div class="section" id="demonstratesSection">
<h1 class="heading" style="color:#003399; font-size:12px"><span>Groupe Building Dynamic Menu</span></h1>
<div class="section" id="sectionSection1">
<p style="margin-top:10px; margin-bottom:5px">Le tableau suivant d&eacute;crit les contr&ocirc;les qui s'affichent dans le groupe<span class="Apple">&nbsp;</span><span class="ui">Building a Dynamic Menu</span><span class="Apple">&nbsp;</span>du ruban
 personnalis&eacute;.</p>
</div>
</div>
<p>&nbsp;</p>
<table border="0" cellspacing="2" cellpadding="5" frame="lhs" style="text-align:left; margin-top:5px; font-size:10px; width:100%">
<tbody>
<tr>
<th style="text-align:left; background-color:#efeff7; color:#000066">
<p style="margin-top:1px; margin-bottom:4px">Contr&ocirc;le</p>
</th>
<th style="text-align:left; background-color:#efeff7; color:#000066">
<p style="margin-top:1px; margin-bottom:4px">Description</p>
</th>
<th style="text-align:left; background-color:#efeff7; color:#000066">
<p style="margin-top:1px; margin-bottom:4px">Action/R&eacute;sultat</p>
</th>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px"><span class="ui">Menu dynamique</span></p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Menu. Un menu est une liste d&eacute;roulante qui peut contenir d'autres contr&ocirc;les de ruban.</p>
<p style="margin-top:1px; margin-bottom:4px">La propri&eacute;t&eacute; Dynamic de ce menu a la valeur<span class="keyword">true</span>. Cela permet la mise &agrave; jour dynamique du menu au moment de l'ex&eacute;cution.</p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Cliquez sur<span class="Apple">&nbsp;</span><span class="ui">Menu dynamique</span>pour afficher un menu de contr&ocirc;les.</p>
</td>
</tr>
<tr>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px"><span class="ui">Case &agrave; cocher</span>,<span class="Apple">&nbsp;</span><span class="ui">Liste d&eacute;roulante</span>,<span class="Apple">&nbsp;</span><span class="ui">Sous-menu</span>,<span class="ui">Galerie</span>,<span class="Apple">&nbsp;</span><span class="ui">Bouton</span>,<span class="ui">S&eacute;parateur</span></p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Jeu de cases &agrave; cocher. Vous pouvez activer ou d&eacute;sactiver une case &agrave; cocher pour activer ou d&eacute;sactiver une option.</p>
<p style="margin-top:1px; margin-bottom:4px">Chaque case &agrave; cocher repr&eacute;sente un contr&ocirc;le de ruban que vous pouvez ajouter &agrave;<span class="Apple">&nbsp;</span><span class="ui">Menu dynamique</span>.</p>
</td>
<td style="background-color:#f7f7ff">
<p style="margin-top:1px; margin-bottom:4px">Cliquez sur une case &agrave; cocher pour ajouter un contr&ocirc;le de ruban &agrave;<span class="Apple">&nbsp;</span><span class="ui">Dynamic Menu dynamique</span>.</p>
</td>
</tr>
</tbody>
</table>
