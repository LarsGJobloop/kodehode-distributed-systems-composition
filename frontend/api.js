const API = "http://api.localhost"

export async function postQuote(quote) {
  const result = await fetch(API + "/quotes", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      
    },
    body: JSON.stringify(quote)
  });

  return result.ok;
}

export async function getQuotes() {
  const result = await fetch(API + "/quotes");
  if (!result.ok) {
    return false;
  };

  const data = await result.json();

  return data;
}