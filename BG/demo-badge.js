// Small "Demo" watermark so it's obvious this deployment isn't the real
// production DRDO system — very light weight, unobtrusive corner badge.
(function () {
  var badge = document.createElement("div");
  badge.textContent = "Demo";
  badge.setAttribute("aria-hidden", "true");
  Object.assign(badge.style, {
    position: "fixed",
    bottom: "10px",
    right: "12px",
    fontFamily: "system-ui, sans-serif",
    fontWeight: "200",
    fontSize: "12px",
    letterSpacing: "0.05em",
    color: "rgba(0, 0, 0, 0.35)",
    background: "rgba(255, 255, 255, 0.6)",
    padding: "2px 8px",
    borderRadius: "4px",
    pointerEvents: "none",
    zIndex: "2147483647",
  });
  document.addEventListener("DOMContentLoaded", function () {
    document.body.appendChild(badge);
  });
})();
