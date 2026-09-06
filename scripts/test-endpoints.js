 
// test-endpoints.js
//
// A simple "smoke test" script for the Kobflow .NET services.
// It doesn't test business logic - it just confirms each container
// is up, listening, and didn't crash on startup.
//
// How it works: for each service, we send a GET request to its
// Swagger UI page. Swagger is a convenient thing to check because
// every service already has it (thanks to ASPNETCORE_ENVIRONMENT=Development),
// it doesn't need a real ID or POST body like your actual domain
// endpoints would, and a 200 response tells us the whole ASP.NET Core
// pipeline (Kestrel + your app) is working.
//
// Requires Node.js 18 or newer (for the built-in `fetch` function -
// check with `node --version` in PowerShell; no npm install needed).
//
// Run it with:
//   node test-endpoints.js
 
const services = [
  { name: "expense-api", port: 5004 },
  { name: "merchant-api", port: 5005 },
  { name: "incomesource-api", port: 5006 },
  { name: "expensecategory-api", port: 5007 },
  { name: "incomecategory-api", port: 5008 },
  { name: "income-api", port: 5009 },
  { name: "kobholder-api", port: 5010 },
];
 
// How long to wait for a single service before giving up (milliseconds).
const TIMEOUT_MS = 5000;
 
async function checkService(service) {
  const url = `http://localhost:${service.port}/swagger/index.html`;
 
  // AbortController lets us cancel the fetch if it takes too long,
  // e.g. if a container is stuck or not running at all.
  const controller = new AbortController();
  const timeoutId = setTimeout(() => controller.abort(), TIMEOUT_MS);
 
  try {
    const response = await fetch(url, { signal: controller.signal });
    clearTimeout(timeoutId);
 
    if (response.ok) {
      // response.ok is true for any 2xx status code
      return { ...service, ok: true, status: response.status };
    } else {
      return { ...service, ok: false, status: response.status };
    }
  } catch (err) {
    clearTimeout(timeoutId);
    // This branch covers: connection refused (container not running),
    // timeout (container stuck/unresponsive), DNS errors, etc.
    return { ...service, ok: false, error: err.message };
  }
}
 
async function main() {
  console.log(`Checking ${services.length} services...\n`);
 
  // Run all checks in parallel rather than one at a time - since these
  // are independent HTTP calls, there's no reason to wait for one to
  // finish before starting the next.
  const results = await Promise.all(services.map(checkService));
 
  for (const result of results) {
    if (result.ok) {
      console.log(`PASS  ${result.name.padEnd(20)} port ${result.port}  (HTTP ${result.status})`);
    } else if (result.status) {
      console.log(`FAIL  ${result.name.padEnd(20)} port ${result.port}  (HTTP ${result.status})`);
    } else {
      console.log(`FAIL  ${result.name.padEnd(20)} port ${result.port}  (${result.error})`);
    }
  }
 
  const failures = results.filter((r) => !r.ok);
  console.log(`\n${results.length - failures.length}/${results.length} services healthy.`);
 
  if (failures.length > 0) {
    // Non-zero exit code so this could later be wired into a CI check
    // or a pre-commit script that fails loudly if something's down.
    process.exitCode = 1;
  }
}
 
main();
 


