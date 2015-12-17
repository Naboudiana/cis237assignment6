$(document).ready(function () {
    //This is a jQuery selector. The '.' is to select a element with the class that
    //follows the '.', the '#' is to select a element with the id that follows the '#'.
    $(".dataRow").click(function (firedEvent) {
        console.log("the event fired");
        //This line will redirect the browser to this URL.   

        console.log(firedEvent.currentTarget.id)

        var id = firedEvent.currentTarget.id;

        document.location = "/Beverages/Edit/" + id;
    });
});

$('.Delete').click(function () {
    var answer = confirm('Confirm deletion of this Page');
    return answer
});