let x = document.querySelectorAll(".fa-regular");
let y = document.querySelectorAll(".showpass");

for (let j = 0; j < x.length; j++) {

  x[j].onclick = function () {

    for (let i = 0; i < y.length; i++) {

      if (y[i].type == "password") {

        y[i].type = "text";
      } else {
        y[i].type = "password";
      }
    }
  }
}




