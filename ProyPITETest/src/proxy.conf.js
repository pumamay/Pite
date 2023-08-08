const PROXY_CONFIG = [
  {
    context: [
      "/Users",
    ],
    target: "https://localhost:7019;",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
