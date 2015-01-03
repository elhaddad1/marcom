<?php
/*
	The Ajax should dump the same code syntax as would be included in the static version
*/
?>
<ul id="tile_001" title="Dynamic Instance" alt="This content was loaded via AJAX">
<?php
	for($i=0;$i<20;$i++)
	{
		echo "<li><a href=\"http://www.seeitfinditgetit.com#{$i}\">See It Find It Get It ".(1+$i)."</a></li>\n";
	}
?>
</ul>
