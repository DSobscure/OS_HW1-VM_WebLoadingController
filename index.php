<?php
    echo "<div>";
        echo "<h1>VM Loading Controller</h1>";
        echo "<form>";
            echo "IP address <input type='text' id='ip' name='ip'><br/>";
            echo "CPU Loading <input type='number' id='loading' name='loading'><br/>";
            echo "Execution Time <input type='number' id='duration' name='duration'><br/>";
            echo "<input type='submit' value='Generate'><br/>";
        echo "</form>";

        echo "VM list";
        echo "<div id='monitor'>";
            echo "<table>";
                echo "<tr> <th>ID</th> <th>Name</th> <th>IP Address</th> <th>State</th> <th>Cpu Loading(%)</th> </tr>";
                echo "<tr> <td>1</td> <td>VM1</td> <td>192.168.122.9</td> <td>running</td> <td>0%</td> </tr>";
                echo "<tr> <td>2</td> <td>VM2</td> <td>192.168.122.209</td> <td>running</td> <td>0%</td> </tr>";
            echo "</table>";
        echo "</dic>";
    echo "</div>";
    echo "\n\nshell_exec";
    $output = shell_exec('virsh domstats --cpu-total VM1');
    echo "\noutput:";
    print_r($output);

?>