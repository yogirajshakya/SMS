<?PHP
	include("scripts/DBConfig.php");

	/* *** Reference website for Conditional Dropdown: http://www.plus2net.com/php_tutorial/php_drop_down_list.php *** */
	/* *** Reference website for PHP & AJAX Tutorial: http://www.php-learn-it.com/tutorials/starting_with_php_and_ajax.html 
	http://woork.blogspot.com/2007/10/login-using-ajax-and-php.html
	
	*** */
	$selectedState=$_GET["state"];
	$strQuery = "select value from MasterData where DataType='City' and ParentValue='$selectedState' order by value;";
	$rsrcResult = mysql_query($strQuery);

	$myarray=array();
	$str="";
	while($nt=mysql_fetch_array($rsrcResult))
	{
		$str=$str . "\"$nt[value]\"".",";
	}
	$str=substr($str,0,(strLen($str)-1)); // Removing the last char , from the string
	echo "new Array($str)";
?>