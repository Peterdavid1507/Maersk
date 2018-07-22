<?php
	try {
		$conn = new PDO("sqlsrv:server = tcp:maersktp035569dbserver.database.windows.net,1433; Database = MaerskTP035569_DB", "tp035569", "P@ssw0rd");
		$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
	}
	catch (PDOException $e) {
		echo $e->getMessage();
		die('<br>Connection Failed.');
	}

	try
	{
		$sql = "UPDATE shipping SET shipping_status = 'Shipping' WHERE shipping_status = 'Approved';";
		$query = $conn->prepare($sql);
		$query->execute();
	}catch (Exception $e)
	{
		echo $e->getMessage();
		die('<br>Execution Failed.');
	}
?>