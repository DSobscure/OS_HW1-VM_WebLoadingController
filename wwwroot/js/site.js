// Write your Javascript code.
function Reload()
{
    $.get("home/Contact",
        {
            
        },
        function(result){ document.getElementById('monitor').innerHTML = result; }
    );
}

setInterval(Reload, 1000);