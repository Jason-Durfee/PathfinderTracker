$(document).ready(function () {
    TableSearch();
});

function TableSearch() {
    $("#txtSearchBox").on("change", Search);
}

function Search() {
    var searchText = document.getElementById("txtSearchBox").value;
    $.ajax({
        type: "GET",
        url: document.getElementById("SearchLink").href,
        data: { "searchText" : searchText },
        success: function (results) {
            var table = document.getElementById("bodyMain");
            $(table).empty();
            $(table).append(results);
        },
        failure: function () {
            alert("Something went wrong with the search");
        },
        complete: function () {
            document.getElementById("txtSearchBox").value = searchText;
        }
    });
}