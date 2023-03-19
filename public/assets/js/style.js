let x = document.querySelectorAll(".fa-regular");
let y = document.querySelectorAll(".showpass");
console.log(x);
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


// SIDEBAR DROPDOWN
const allDropdown = document.querySelectorAll('#sidebar .side-dropdown');
const sidebar = document.getElementById('sidebar');

allDropdown.forEach(item => {
  const a = item.parentElement.querySelector('a:first-child');
  a.addEventListener('click', function (e) {
    e.preventDefault();

    if (!this.classList.contains('active')) {
      allDropdown.forEach(i => {
        const aLink = i.parentElement.querySelector('a:first-child');

        aLink.classList.remove('active');
        i.classList.remove('show');
      })
    }

    this.classList.toggle('active');
    item.classList.toggle('show');
  })
})
//check Bao Cao
let caret = document.querySelectorAll('.box-icon');
let dropd = document.querySelectorAll('.dropdown-content');
for (let i = 0; i < dropd.length; ++i) {
  caret[i].onclick = async function () {
    if (dropd[i].style.display == 'block') {
      dropd[i].style.display = 'none';
    }
    else {
      dropd[i].style.display = 'block';
    }
  }
}
//Check Danh Sach Thiet Lap



// console.log(btn_de)
// for (let i = 0; i < cont.length; ++i) {
//   btn_de[i].onclick = function () {

//     if (cont[i].style.display == 'block') {
//       cont[i].style.display = 'none';
//     }
//     else {
//       cont[i].style.display = 'block';
//     }

//   }

//   cont[i].onclick = function () {
//     cont[i].style.display = 'none';
//   }
// }

//   if (btn_de.classList.contains("btn-detail")) {
//     btn_de.classList.remove("btn-detail");
//     cont.classList.add("content1");
//   }
//   else {
//     btn_de.classList.add("btn-detail");
//     cont.classList.remove("content1");
//   }
// }


// var myModal = document.getElementById('myModal')
// var myInput = document.getElementById('myInput')

// myModal.addEventListener('shown.bs.modal', function () {
//   myInput.focus()
// })

