import * as api from './api.js'

/**@type {HTMLTemplateElement} */
const quoteTemplate = document.querySelector("#quote-template")
const list = document.querySelector("#quotes")
document.querySelector("#quote-form")
  .addEventListener("submit", handleFormSubmission)

updateView()

async function handleFormSubmission(submitEvent) {
  console.log("HAndling")
  submitEvent.preventDefault();
  /**@type {HTMLFormElement} */
  const form = submitEvent.target;
  /**@type {HTMLButtonElement} */
  const button = form.querySelector("button[type='submit']")
  const formData = new FormData(form);

  button.disabled = true;

  const newQuote = {
    id: Math.floor(Math.random() * 1024), // TODO! Should be handled in the backend
    content: formData.get("content").toString(),
    author: formData.get("author").toString(),
  };

  await api.postQuote(newQuote);
  await updateView();
  form.reset();
  button.disabled = false;
}

async function updateView() {
  const quotes = await api.getQuotes()

  const cards = quotes.map(quote => createCard(quote))
  list.innerHTML = ""; // Hacky clearing of elements
  list.append(...cards);
}

function createCard(quote) {
  const card = quoteTemplate.content.cloneNode(true);
  const content = card.querySelector("[data-qoute='content']");
  const author = card.querySelector("[data-qoute='author']");

  content.textContent = quote.content;
  author.textContent = quote.author;

  return card;
}

