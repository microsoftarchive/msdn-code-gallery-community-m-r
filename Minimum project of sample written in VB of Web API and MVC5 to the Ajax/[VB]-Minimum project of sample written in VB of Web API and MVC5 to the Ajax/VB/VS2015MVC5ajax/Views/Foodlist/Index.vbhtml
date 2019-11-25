@Code
    ViewData("Title") = "Index"
End Code

<h2>Please Push Button</h2>

<button id="display" class="btn btn-success">Display List</button>

<h2>Foods List</h2>
<div id="list"></div>

<h2>End of List</h2>

@Section scripts
<script>
    $("#display").click(function () {
        var url = "/api/foods/";
        $.getJSON(url)
            .done(function (data) {
                var html = "<ul>";
                $.each(data, function (index, value) {
                    html += "<li>" + value + "</li>";
                })
                html += "</ul>";
                $("#list").html(html);
            })
            .fail(function(data){
                $("#list").text("Error on Ajax!!!");
            });
    });

</script>
End section


