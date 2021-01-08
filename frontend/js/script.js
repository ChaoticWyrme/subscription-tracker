document.querySelector('.add_subscription_form').addEventListener("submit", e => {
  //make post request
  e.preventDefault()

  const data = JSON.stringify({
    name: document.getElementById('sub_name').value,
    price: document.getElementById('price').value,
    url: document.getElementById('sub_url').value,
  })

  const xhr = new XMLHttpRequest()
  xhr.open("POST", "http://chaoticwyrme-001-site1.itempurl.com/")
  xhr.onreadystatechange = function() {
    if (xhr.readyState === xhr.DONE) {
      window.location.href = "home.html"
    }
  }
  xhr.send(data)
})

// function DoSubmit(){
//   //post request to the api
//   window.location.href = "home.html"
//   return true;
// }
