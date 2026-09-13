# MSP2TOOL Clean 1.8.38

Merged the functional 1.8.38 app features into the privacy-clean build.

Included: PetClone, Homes Harvest/dynamic Home loading, dynamic Emoji Pack, StarQuiz, DM Spam Shield/Lockdown, Outfit Copy/Restore/Emergency, Avatar/Room image sync, Pet nickname UI and related state/cache improvements.

Privacy: keeps the clean background/bridge and does not include the vendor credential-vault, device binding, remote gate, heartbeat or vendor host permissions. Chrome webRequest is not enabled.

The app UI may still contain a clickable vendor website label inherited from the upstream UI; it does not create a network request by itself.

### 1.8.40 Performance Update

- Added local D3/emoji caching with `_d3Cached`.
- Added background D3 preloading with `_warmD3Background`.
- Added local pack text loading helper `_fetchPackText`.
- Added UI/service-worker yielding via `_yieldUi`.
- Added `xb:prefetch` support for local data packs.
- No vendor credential, telemetry, vault, heartbeat, remote gate, or external configuration functionality was added.
