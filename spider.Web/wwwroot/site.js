
// For select all button on Index page
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

// For Search on Index page
document.getElementById('search-box').addEventListener("keyup", function()

{
  t = document.getElementById('search-box'); //
  let text = t.value.toUpperCase();          //
  c = document.querySelectorAll("[id^='car-model']")
  for (var i = 0; i < c.length; i++) {
    a = c[i]
    txtValue = a.textContent || a.innerText;
    if (txtValue.toUpperCase().indexOf(text) > -1) {
      a.parentElement.parentElement.parentElement.style.display = "";
    } else {
      a.parentElement.parentElement.parentElement.style.display = "none";
    }
  }
})

function spinner() {
  document.getElementsByClassName("loader")[0].style.display = "block";
}