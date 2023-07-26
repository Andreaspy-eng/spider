
// For select all cars button on Index page
function SetAllCheckBoxes(obj)
{
    var c = new Array();
    c = document.getElementsByName("checkboxes");
  
    for (var i = 0; i < c.length; i++) {
        if (c[i].type == 'checkbox') {
            c[i].checked = obj.checked;
        }
    }
}
// For select all invoices button on Index page
function SetAllCheckBoxesTodayInvoices(obj)
{
    var c = new Array();
    c = document.querySelectorAll("[id^='checkboxes-today']");
    for (var i = 0; i < c.length; i++) {
        if (c[i].type == 'checkbox') {
            c[i].checked = obj.checked;
        }
    }
}

function SetAllCheckBoxesOtherInvoices(obj)
{
    var c = new Array();
    c = document.querySelectorAll("[id^='checkboxes-other']");
    for (var i = 0; i < c.length; i++) {
        if (c[i].type == 'checkbox') {
            c[i].checked = obj.checked;
        }
    }
}
// For Search cars on Index page
document.getElementById('search-box').addEventListener("keyup", function()

{
  t = document.getElementById('search-box'); 
  let text = t.value.toUpperCase();
  // name="checkboxes-cars"       
  c = document.querySelectorAll("[id^='car-number']")
  d = document.querySelectorAll("[id^='car-model']")

  for (var i = 0; i < c.length; i++) {
    number = c[i]
    model = d[i]
    txtValue = number.textContent || number.innerText;
    modelValue = model.textContent || model.innerText;
    if (txtValue.toUpperCase().indexOf(text) > -1 || modelValue.toUpperCase().indexOf(text) > -1 ) {
      number.parentElement.parentElement.parentElement.style.display = "";
    } else {
      number.parentElement.parentElement.parentElement.style.display = "none";
    }
  }
})

// For Search invoice on Index page
document.getElementById('search-invoices').addEventListener("keyup", function()

{
  t = document.getElementById('search-invoices'); 
  let text = t.value.toUpperCase();          
  c = document.querySelectorAll("[id^='counterparty-name']")
  d = document.querySelectorAll("[id^='counterparty-address']")
  for (var i = 0; i < c.length; i++) {
    a = c[i]
    b = d[i]
    txtValue = a.textContent || a.innerText;
    AddressValue = b.textContent || b.innerText;
    if (txtValue.toUpperCase().indexOf(text) > -1 || AddressValue.toUpperCase().indexOf(text) > -1 ) {
      a.parentElement.style.display = "";
    } else {
      a.parentElement.style.display = "none";
    }
  }
})

function spinner() {
  document.getElementsByClassName("loader")[0].style.display = "block";
}

// Checked by click on row
var tr = document.getElementsByTagName("tr"),
    i = tr.length;

while (i--) {
    tr[i].onclick = clickTr;
}

function clickTr(event) {
    var inputs = this.getElementsByTagName('input');
    for (var x = 0; x < inputs.length; x++) {
        if (inputs[x] !== event.target && inputs[x].type == 'checkbox') {
            inputs[x].checked = !(inputs[x].checked);
        }
    }
}

// Checked by click on card
var cards = document.querySelectorAll("[id^='car-card']"),
    i = cards.length;

while (i--) {
  cards[i].onclick = clickCard;
}

function clickCard(event) {
    var inputs = this.getElementsByTagName('input');
    for (var x = 0; x < inputs.length; x++) {
        if (inputs[x] !== event.target && inputs[x].type == 'checkbox') {
            inputs[x].checked = !(inputs[x].checked);
        }
    }
}