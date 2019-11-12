# Relational Database Example
## Requires
- Visual Studio 2012
## License
- Apache License, Version 2.0
## Technologies
- VB.Net
## Topics
- SQL
- LINQ
- Databases
- Many to many relationships
## Updated
- 08/09/2013
## Description

<p><span style="font-size:small"><img id="94258" src="94258-10-08-2013%2000.48.36.jpg" alt="" width="493" height="335"></span></p>
<p><span style="font-size:small">This is a Relational Database Example that I wrote in response to a question on another website.</span><br>
<span style="font-size:small">The Database has 3 Tables. 2 Entity Tables, Student and Course...</span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small"><img id="94255" src="94255-10-08-2013%2000.43.22.jpg" alt="" width="673" height="247"></span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small"><img id="94256" src="94256-10-08-2013%2000.45.52.jpg" alt="" width="495" height="287"></span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small">...and Joining_Table which contains the many to many relationships.</span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small"><img id="94257" src="94257-10-08-2013%2000.47.19.jpg" alt="" width="247" height="345"></span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small">At Form_Load, I query the Student Table &#43; read it in its entirety into a Datatable, then add all of the unique numbers from the Student_index Field to a DomainUpDown Control.</span><br>
<span style="font-size:small">On DomainUpDown_SelectedItemChanged, I used LINQ to query the Form level Datatable that I populated in Form_Load, and set the text of 4 Labels with the Fields of the student record at that Student_index.</span><br>
<span style="font-size:small">Using the Student_index, I used SQL to query the Joining_Table Table, to get a list of all of the values of the Course_code Field that the particular student is enrolled in. Using this information, I queried the Course Table, to
 find all records with a Course_code value that matched any of the values from the previous query, and populated a Datatable, which I then set as the DataGridView DataSource.</span><br>
<span style="font-size:small">The final step was to use the Datatable's Compute method to sum the Fees field and set a 5th Label Text with Total Fees.</span></p>
<p><span style="font-size:small">This example shows how to query Databases and display records both with and without Databinding.</span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small">&nbsp;</span></p>
<p><span style="font-size:small">&nbsp;</span></p>
