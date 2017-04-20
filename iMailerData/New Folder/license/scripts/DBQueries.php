<?php
include("DBConfig.php");

$result = mssql_query("SELECT SalRep As [Sales Representative], ComPercent As [Commission Percentage] FROM [Builder Channel] Order By SalRep");
$fields_num = mssql_num_fields($result);


echo "<h1><u>Commissions for Builder Channel Sales representative<u></h1>";

echo "<table border='1'><tr>";
// printing table headers
for($i=0; $i<$fields_num; $i++)
{
	$field = mssql_fetch_field($result);
	echo "<td>{$field->name}</td>";
}
echo "</tr>\n";

// printing table rows
while($row = mssql_fetch_row($result))
{
echo "<tr>";
// $row is array… foreach( .. ) puts every element
// of $row to $cell variable
foreach($row as $cell)
echo "<td>$cell</td>";

echo "</tr>\n";
}
mssql_free_result($result);
echo "</table>"

function dropdown($name, $options, $strTableName, $strOrderField, $strMethod="asc")
{
	echo "<select name=\"$name\">\n";
	echo "<option value=\"NULL\">Select Value</option>\n";
	
	$strQuery = "select $options from $strTableName order by $strOrderField $strMethod";
	$rsrcResult = mssql_query($strQuery);

	while($arrayRow = mssql_fetch_assoc($rsrcResult))
	{
		//$strA = $arrayRow["$intIdField"];
		$strB = $arrayRow["$options"];
		//echo "<option value=\"$strA\">$strB</option>\n";
		echo "<option value=\"ABC\">$strB</option>\n";
	}
	
	echo "</select>";
}
mssql_close($db);
?>