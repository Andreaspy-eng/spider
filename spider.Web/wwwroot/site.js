function SetAllCheckBoxes(obj)
{
    var c = new Array();
    var cards = document.querySelectorAll('.show');
    if (cards.length < 1) {
        c = document.getElementsByName("checkboxes");
    }
    else {
        for (var i = 0; i < cards.length; i++) {
            c.push(cards[i].childNodes[1].firstElementChild);
        }
    }
    for (var i = 0; i < c.length; i++) {
        if (c[i].type == 'checkbox') {
            c[i].checked = obj.checked;
        }
    }
}
