# .NET 10 Upgrade Plan

## Table of Contents

- [Executive Summary](#executive-summary)
- [Migration Strategy](#migration-strategy)
- [Detailed Dependency Analysis](#detailed-dependency-analysis)
- [Project-by-Project Plans](#project-by-project-plans)
  - [AspNetCore.RequestCounter](#aspnetcorerequestcounter)
  - [AspNetCore.RequestCounterTestBlazorApp](#aspnetcorerequestcountertestblazorapp)
  - [AspNetCore.RequestCounterTestWeb](#aspnetcorerequestcountertestweb)
- [Package Update Reference](#package-update-reference)
- [Breaking Changes Catalog](#breaking-changes-catalog)
- [Risk Management](#risk-management)
- [Testing & Validation Strategy](#testing--validation-strategy)
- [Complexity & Effort Assessment](#complexity--effort-assessment)
- [Source Control Strategy](#source-control-strategy)
- [Success Criteria](#success-criteria)

---

## Executive Summary

### Scenario Description

Upgrade all projects in the AspNetCore.RequestCounter solution from .NET 7 to .NET 10 (Long Term Support).

### Scope

**Projects Affected**: 3
- AspNetCore.RequestCounter (Class Library) - 187 LOC
- AspNetCore.RequestCounterTestBlazorApp (Blazor App) - 169 LOC
- AspNetCore.RequestCounterTestWeb (Razor Pages App) - 244 LOC

**Current State**: All projects targeting net7.0
**Target State**: All projects targeting net10.0

### Discovered Metrics

- **Total Projects**: 3 (all SDK-style)
- **Total Lines of Code**: 600
- **Dependency Depth**: 2 levels (linear chain)
- **Circular Dependencies**: None
- **NuGet Packages Requiring Update**: 1 (Microsoft.AspNetCore.Http.Abstractions)
- **Security Vulnerabilities**: 0
- **API Behavioral Changes**: 2 instances (UseExceptionHandler method)
- **Files with Incidents**: 5 out of 23 total files

### Complexity Classification

**SIMPLE Solution** - Ideal for All-at-Once Strategy

**Justification**:
- Small solution (3 projects, 600 LOC)
- All projects currently on .NET 7.0
- Linear dependency structure (no complex relationships)
- All projects marked as Low difficulty
- No security vulnerabilities
- Minimal package updates required (1 package)
- Homogeneous codebase with consistent patterns

### Selected Strategy

**All-At-Once Strategy** - All projects upgraded simultaneously in single atomic operation.

**Rationale**:
- Solution size is small (3 projects)
- All projects currently on modern .NET (7.0)
- Simple, clear dependency structure (linear chain)
- Low external dependency complexity (only 1 package to update)
- All behavioral changes are low-impact (testing only)
- Unified upgrade minimizes coordination overhead

### Expected Iterations

This plan will be completed in approximately 6-7 iterations:
- Phase 1: Discovery & Classification (3 iterations) ✓
- Phase 2: Foundation (3 iterations)
- Phase 3: Dynamic Detail Generation (1 iteration - all projects batched together)

### Critical Issues

**Package Updates**:
- Microsoft.AspNetCore.Http.Abstractions (2.2.0) - outdated, needs update

**Behavioral Changes**:
- UseExceptionHandler API has behavioral changes in .NET 10 (2 instances across Blazor and Razor Pages apps)

**Recommended Approach**: All-at-Once atomic upgrade with comprehensive testing phase

---

## Migration Strategy

### Approach Selection: All-At-Once Strategy

**Selected Strategy**: All projects in the solution will be upgraded simultaneously in a single coordinated operation.

**Justification**:

This solution meets all ideal conditions for All-at-Once approach:
- **Small solution size**: 3 projects (well under 30-project threshold)
- **Modern starting point**: All projects currently on .NET 7.0 (modern .NET, not .NET Framework)
- **Homogeneous codebase**: All projects use consistent patterns, all SDK-style
- **Low dependency complexity**: Only 1 external package requiring update
- **Clear assessment results**: All packages have known compatibility status
- **Simple structure**: Linear dependency chain, no circular dependencies
- **Low risk profile**: All projects marked as Low difficulty, no security vulnerabilities

### All-at-Once Strategy Rationale

**Advantages for this solution**:
- **Fastest completion**: Single atomic operation, no multi-targeting overhead
- **Simplified coordination**: All developers work with same framework version immediately
- **Clean dependency resolution**: No intermediate states with mixed framework versions
- **Reduced testing complexity**: Single comprehensive test pass instead of phase-by-phase validation
- **Minimal planning overhead**: No need to coordinate inter-phase dependencies

**Approach Execution**:
1. Update all 3 project files from net7.0 to net10.0 simultaneously
2. Update Microsoft.AspNetCore.Http.Abstractions package in RequestCounter project
3. Restore dependencies for entire solution
4. Build entire solution and address all compilation errors in single pass
5. Address behavioral changes (UseExceptionHandler) in both test applications
6. Verify complete solution builds with 0 errors
7. Run comprehensive testing phase

### Dependency-Based Ordering

While the upgrade is atomic, the logical dependency order is respected during validation:

**Logical Order** (informational):
1. **AspNetCore.RequestCounter** - Base library (no dependencies)
2. **AspNetCore.RequestCounterTestBlazorApp** - Depends on RequestCounter
3. **AspNetCore.RequestCounterTestWeb** - Depends on both projects

This ordering ensures that when building, the dependency chain is resolved correctly.

### Parallel vs Sequential Execution

**Execution Model**: Single atomic batch

All project file updates and package updates happen simultaneously. The build system will automatically resolve the dependency order during compilation.

**No Intermediate States**: The upgrade moves directly from "all projects on net7.0" to "all projects on net10.0" with no mixed-framework intermediate state.

### Risk Management Alignment

The All-at-Once approach is low-risk for this solution because:
- No breaking API changes detected (only behavioral changes)
- Small codebase (600 LOC total)
- Low complexity across all projects
- Single package update required
- No security vulnerabilities to address urgently

---

## Detailed Dependency Analysis

### Dependency Graph Summary

The solution has a simple linear dependency structure:

```
RequestCounterTestWeb (Razor Pages App)
  ├─> RequestCounterTestBlazorApp (Blazor App)
  │     └─> RequestCounter (Class Library)
  └─> RequestCounter (Class Library)
```

**Dependency Relationships**:
- **AspNetCore.RequestCounter**: Leaf project (0 dependencies, 2 dependants)
- **AspNetCore.RequestCounterTestBlazorApp**: Intermediate project (1 dependency, 1 dependant)
- **AspNetCore.RequestCounterTestWeb**: Root application (2 dependencies, 0 dependants)

### Migration Phase Groupings

For All-at-Once Strategy, all projects are upgraded simultaneously in a single atomic operation:

**Phase 1: Atomic Upgrade - All Projects**
- AspNetCore.RequestCounter
- AspNetCore.RequestCounterTestBlazorApp
- AspNetCore.RequestCounterTestWeb

All project files are updated to net10.0, all packages are updated, and all compilation errors are fixed in one coordinated operation.

### Critical Path

Since this is an all-at-once upgrade, there is no sequential critical path. However, the logical dependency order is:

1. **Base Library** (AspNetCore.RequestCounter) - foundational class library
2. **Blazor Application** (AspNetCore.RequestCounterTestBlazorApp) - depends on base library
3. **Razor Pages Application** (AspNetCore.RequestCounterTestWeb) - depends on both

This ordering is informational only; actual upgrade happens atomically across all projects.

### Circular Dependencies

**None detected** - The dependency structure is a clean directed acyclic graph (DAG).

---

## Project-by-Project Plans

This section provides detailed migration specifications for each project in dependency order (informational - actual upgrade is atomic).

### AspNetCore.RequestCounter

**Project Type**: Class Library (SDK-style)
**Current State**: net7.0, 187 LOC, 6 files, 1 file with incidents
**Target State**: net10.0

#### Current State Details

- **Target Framework**: net7.0
- **Dependencies**: 0 project dependencies
- **Dependants**: 2 (AspNetCore.RequestCounterTestBlazorApp, AspNetCore.RequestCounterTestWeb)
- **NuGet Packages**: 1
  - Microsoft.AspNetCore.Http.Abstractions 2.2.0 (⚠️ Outdated)
- **Code Files**: 6
- **Files with Incidents**: 1
- **Lines of Code**: 187
- **Risk Level**: 🟢 Low

#### Target State

- **Target Framework**: net10.0
- **Package Updates**: Microsoft.AspNetCore.Http.Abstractions → compatible .NET 10 version
- **Expected Code Changes**: None (unless package update introduces API changes)

#### Migration Steps

**1. Prerequisites**
- Ensure .NET 10 SDK is installed
- Verify solution builds successfully on net7.0 before upgrade

**2. Framework Update**
- Update `<TargetFramework>` in `AspNetCore.RequestCounter.csproj` from `net7.0` to `net10.0`

**3. Package Updates**

| Package Name | Current Version | Target Version | Reason |
|-------------|-----------------|----------------|--------|
| Microsoft.AspNetCore.Http.Abstractions | 2.2.0 | Latest compatible with .NET 10 | Outdated package, framework compatibility |

**Action**: Remove explicit PackageReference if abstraction is now included in .NET 10 runtime, or update to latest compatible version.

**4. Expected Breaking Changes**

- **None detected** in assessment
- However, if Microsoft.AspNetCore.Http.Abstractions is updated:
  - Review release notes for API changes
  - Check for deprecated types/methods
  - Verify IHttpContextAccessor and related abstractions remain compatible

**5. Code Modifications**

**Anticipated Changes**: Minimal to none

**Areas to Review**:
- HTTP context abstractions usage
- Any middleware components using abstractions
- Dependency injection configuration for HTTP context

**Known Patterns Requiring Update**: None identified in assessment

**6. Testing Strategy**

**Unit Tests**: None identified in solution
**Integration Tests**: Verify through dependent projects (Blazor and Razor Pages apps)

**Manual Validation**:
- Build RequestCounter project independently
- Verify no compilation errors or warnings
- Ensure dependent projects (Blazor and Razor Pages) can reference and use the library
- Test HTTP context abstraction functionality through consuming applications

**7. Validation Checklist**

- [ ] Project file updated to net10.0
- [ ] Microsoft.AspNetCore.Http.Abstractions package updated or removed
- [ ] `dotnet restore` completes without errors
- [ ] `dotnet build` completes with 0 errors
- [ ] `dotnet build` completes with 0 warnings
- [ ] Dependent projects (Blazor and Razor Pages) build successfully
- [ ] No NuGet package conflicts reported
- [ ] Library functionality verified through consuming applications

---

### AspNetCore.RequestCounterTestBlazorApp

**Project Type**: Blazor Application (SDK-style)
**Current State**: net7.0, 169 LOC, 29 files, 2 files with incidents
**Target State**: net10.0

#### Current State Details

- **Target Framework**: net7.0
- **Dependencies**: 1 project (AspNetCore.RequestCounter)
- **Dependants**: 1 (AspNetCore.RequestCounterTestWeb)
- **NuGet Packages**: Framework-provided packages only
- **Code Files**: 29
- **Files with Incidents**: 2
- **Lines of Code**: 169
- **API Issues**: 1 behavioral change (UseExceptionHandler)
- **Risk Level**: 🟢 Low

#### Target State

- **Target Framework**: net10.0
- **Package Updates**: None required (all framework packages update automatically)
- **Expected Code Changes**: Validate UseExceptionHandler behavior

#### Migration Steps

**1. Prerequisites**
- AspNetCore.RequestCounter must be upgraded first (or simultaneously in All-at-Once)
- Ensure .NET 10 SDK is installed
- Verify application runs correctly on net7.0 before upgrade

**2. Framework Update**
- Update `<TargetFramework>` in `AspNetCore.RequestCounterTestBlazorApp.csproj` from `net7.0` to `net10.0`

**3. Package Updates**

**No explicit package updates required** - all ASP.NET Core packages are framework-provided and will automatically resolve to .NET 10 versions.

**4. Expected Breaking Changes**

**API Behavioral Change Detected**:

| API | Category | Impact | Location |
|-----|----------|--------|----------|
| `Microsoft.AspNetCore.Builder.ExceptionHandlerExtensions.UseExceptionHandler(IApplicationBuilder, string)` | 🔵 Behavioral Change | Low - Runtime testing required | 2 files with incidents |

**What Changed**:
- The UseExceptionHandler middleware may have behavioral changes in .NET 10
- Error handling pipeline behavior may differ
- Exception propagation or logging behavior may be modified

**Required Actions**:
- Review .NET 10 breaking changes documentation for UseExceptionHandler
- Test error handling scenarios thoroughly
- Verify custom error pages still function correctly
- Check that exceptions are handled as expected

**5. Code Modifications**

**Anticipated Changes**: Minimal configuration adjustments

**Files to Review** (based on incidents):
- 2 files containing UseExceptionHandler calls
- Likely in Program.cs or Startup.cs (middleware configuration)

**Areas to Review**:
- Middleware pipeline configuration
- Exception handler endpoint configuration
- Custom error page routing
- Error handling behavior in development vs production

**Potential Code Updates**:
```csharp
// Before (.NET 7)
app.UseExceptionHandler("/Error");

// After (.NET 10) - May require additional configuration
app.UseExceptionHandler("/Error");
// Review documentation for any new options or behavioral changes
```

**6. Testing Strategy**

**Unit Tests**: None identified in solution

**Integration Testing**:
- Test normal page navigation
- **Test exception handling scenarios**:
  - Trigger unhandled exceptions
  - Verify error page displays correctly
  - Confirm exception details are logged appropriately
  - Test both development and production exception handling

**Blazor-Specific Testing**:
- Test component rendering after upgrade
- Verify interactive components function correctly
- Test SignalR connection (if used for Blazor Server)
- Verify routing works as expected
- Test state management and cascading parameters

**Manual Validation**:
- Run application locally
- Navigate through all pages/components
- Trigger error conditions deliberately
- Verify error handling matches expected behavior
- Check browser console for JavaScript errors
- Test with different browsers if possible

**7. Validation Checklist**

- [ ] Project file updated to net10.0
- [ ] AspNetCore.RequestCounter project reference resolves correctly
- [ ] `dotnet restore` completes without errors
- [ ] `dotnet build` completes with 0 errors
- [ ] `dotnet build` completes with 0 warnings
- [ ] Application starts without errors
- [ ] All Blazor components render correctly
- [ ] Exception handler middleware functions correctly
- [ ] Error pages display as expected
- [ ] No console errors in browser
- [ ] Interactive components respond to user input
- [ ] Routing works correctly
- [ ] Performance is acceptable (no degradation)

---

### AspNetCore.RequestCounterTestWeb

**Project Type**: Razor Pages Application (SDK-style)
**Current State**: net7.0, 244 LOC, 16 files, 2 files with incidents
**Target State**: net10.0

#### Current State Details

- **Target Framework**: net7.0
- **Dependencies**: 2 projects (AspNetCore.RequestCounter, AspNetCore.RequestCounterTestBlazorApp)
- **Dependants**: 0 (root application)
- **NuGet Packages**: Framework-provided packages only
- **Code Files**: 16
- **Files with Incidents**: 2
- **Lines of Code**: 244
- **API Issues**: 1 behavioral change (UseExceptionHandler)
- **Risk Level**: 🟢 Low

#### Target State

- **Target Framework**: net10.0
- **Package Updates**: None required (all framework packages update automatically)
- **Expected Code Changes**: Validate UseExceptionHandler behavior

#### Migration Steps

**1. Prerequisites**
- Both dependent projects must be upgraded first (or simultaneously in All-at-Once)
- Ensure .NET 10 SDK is installed
- Verify application runs correctly on net7.0 before upgrade

**2. Framework Update**
- Update `<TargetFramework>` in `AspNetCore.RequestCounterTestWeb.csproj` from `net7.0` to `net10.0`

**3. Package Updates**

**No explicit package updates required** - all ASP.NET Core packages are framework-provided and will automatically resolve to .NET 10 versions.

**4. Expected Breaking Changes**

**API Behavioral Change Detected**:

| API | Category | Impact | Location |
|-----|----------|--------|----------|
| `Microsoft.AspNetCore.Builder.ExceptionHandlerExtensions.UseExceptionHandler(IApplicationBuilder, string)` | 🔵 Behavioral Change | Low - Runtime testing required | 2 files with incidents |

**What Changed**:
- The UseExceptionHandler middleware may have behavioral changes in .NET 10
- Error handling pipeline behavior may differ
- Exception propagation or logging behavior may be modified

**Required Actions**:
- Review .NET 10 breaking changes documentation for UseExceptionHandler
- Test error handling scenarios thoroughly
- Verify custom error pages still function correctly
- Check that exceptions are handled as expected

**5. Code Modifications**

**Anticipated Changes**: Minimal configuration adjustments

**Files to Review** (based on incidents):
- 2 files containing UseExceptionHandler calls
- Likely in Program.cs or Startup.cs (middleware configuration)

**Areas to Review**:
- Middleware pipeline configuration
- Exception handler endpoint configuration
- Error.cshtml page (Razor Pages error page)
- Error handling behavior in development vs production
- Integration with referenced Blazor app

**Potential Code Updates**:
```csharp
// Before (.NET 7)
app.UseExceptionHandler("/Error");

// After (.NET 10) - May require additional configuration
app.UseExceptionHandler("/Error");
// Review documentation for any new options or behavioral changes
```

**Unique Considerations**:
- This project references both the library AND the Blazor app
- Ensure the Blazor app integration continues to work correctly
- Verify any shared components or services between Razor Pages and Blazor function properly

**6. Testing Strategy**

**Unit Tests**: None identified in solution

**Integration Testing**:
- Test normal page navigation
- **Test exception handling scenarios**:
  - Trigger unhandled exceptions in Razor Pages
  - Verify Error.cshtml displays correctly
  - Confirm exception details are logged appropriately
  - Test both development and production exception handling
- **Test Blazor integration**:
  - Verify Blazor app components render in Razor Pages context
  - Test navigation between Razor Pages and Blazor components

**Razor Pages-Specific Testing**:
- Test page models (PageModel classes)
- Verify server-side rendering works correctly
- Test form submissions and model binding
- Verify tag helpers function correctly
- Test partial views and view components
- Verify layout pages render correctly

**Manual Validation**:
- Run application locally
- Navigate through all Razor Pages
- Trigger error conditions deliberately
- Verify error handling matches expected behavior
- Test integration points with Blazor app
- Test integration points with RequestCounter library
- Verify any request counting functionality works

**7. Validation Checklist**

- [ ] Project file updated to net10.0
- [ ] Both project references (RequestCounter and Blazor app) resolve correctly
- [ ] `dotnet restore` completes without errors
- [ ] `dotnet build` completes with 0 errors
- [ ] `dotnet build` completes with 0 warnings
- [ ] Application starts without errors
- [ ] All Razor Pages render correctly
- [ ] Exception handler middleware functions correctly
- [ ] Error.cshtml page displays as expected
- [ ] Form submissions work correctly
- [ ] Model binding functions as expected
- [ ] Integration with Blazor app works correctly
- [ ] Integration with RequestCounter library works correctly
- [ ] Request counting functionality verified (if applicable)
- [ ] No runtime errors or exceptions
- [ ] Performance is acceptable (no degradation)

---

## Package Update Reference

### Overview

This solution has minimal external package dependencies. Only 1 package requires attention during the upgrade.

### Package Updates by Scope

#### Explicit Package Updates

| Package | Current Version | Target Version | Projects Affected | Update Reason |
|---------|-----------------|----------------|-------------------|---------------|
| Microsoft.AspNetCore.Http.Abstractions | 2.2.0 | Latest .NET 10 compatible OR remove | 1 (AspNetCore.RequestCounter) | Outdated package, framework compatibility |

#### Framework-Provided Packages

All ASP.NET Core framework packages are implicitly provided by the .NET 10 SDK and will automatically resolve to .NET 10 versions:
- Microsoft.AspNetCore.App (implicit)
- Microsoft.NETCore.App (implicit)

**Projects Affected**: AspNetCore.RequestCounterTestBlazorApp, AspNetCore.RequestCounterTestWeb

**Action Required**: None - automatic resolution upon framework update

### Package Update Details

#### Microsoft.AspNetCore.Http.Abstractions

**Current Version**: 2.2.0 (very outdated - from .NET Core 2.2 era)
**Target Version**: To be determined during upgrade
**Projects**: AspNetCore.RequestCounter

**Update Strategy**:

Option 1 (Recommended): **Remove explicit package reference**
- In .NET 10, HTTP abstractions are typically included in the framework
- Remove `<PackageReference Include="Microsoft.AspNetCore.Http.Abstractions" Version="2.2.0" />` from project file
- Types like `IHttpContextAccessor` should resolve from framework directly

Option 2: **Update to latest compatible version**
- If explicit reference is still needed, update to latest version compatible with .NET 10
- Check NuGet.org for latest version supporting net10.0
- Update version in PackageReference

**Validation**:
- After update/removal, ensure `IHttpContextAccessor` and related types still resolve
- Build project and verify no missing type errors
- Test functionality in consuming applications

### Package Compatibility Matrix

| Package | .NET 7 Version | .NET 10 Status | Action Required |
|---------|----------------|----------------|-----------------|
| Microsoft.AspNetCore.Http.Abstractions | 2.2.0 | Included in framework | Remove or update |

### No Security Vulnerabilities

✅ **Good news**: No packages with known security vulnerabilities were detected in the assessment.

### Post-Upgrade Package Verification

After completing the upgrade, verify package compatibility:

```bash
# Restore packages for entire solution
dotnet restore

# Check for package vulnerabilities
dotnet list package --vulnerable

# Check for deprecated packages
dotnet list package --deprecated

# Check for outdated packages
dotnet list package --outdated
```

---

## Breaking Changes Catalog

### Overview

The assessment identified **no binary or source incompatible breaking changes**. However, there are **2 instances of behavioral changes** that require validation through testing.

### Breaking Changes Summary by Category

| Category | Count | Impact Level | Action Required |
|----------|-------|--------------|-----------------|
| 🔴 Binary Incompatible | 0 | High - Code changes required | None |
| 🟡 Source Incompatible | 0 | Medium - Re-compilation needed | None |
| 🔵 Behavioral Changes | 2 | Low - Runtime testing required | Validate behavior |
| ✅ Compatible APIs | 2,742 | None | No action needed |

### Behavioral Changes Detail

#### UseExceptionHandler Middleware Behavioral Change

**API**: `Microsoft.AspNetCore.Builder.ExceptionHandlerExtensions.UseExceptionHandler(IApplicationBuilder, string)`

**Category**: 🔵 Behavioral Change
**Severity**: Low
**Instances**: 2 (one in each test application)

**Affected Projects**:
1. AspNetCore.RequestCounterTestBlazorApp (1 instance)
2. AspNetCore.RequestCounterTestWeb (1 instance)

**What May Change**:

The UseExceptionHandler middleware may have behavioral differences in .NET 10 compared to .NET 7:

- **Error Processing Pipeline**: The way exceptions flow through the middleware pipeline may differ
- **Status Code Handling**: Default status codes for certain exception types may change
- **Exception Re-execution**: The path re-execution behavior when routing to error pages may be modified
- **Logging Behavior**: Exception logging integration or verbosity may change
- **Development vs Production**: Differences in exception handling between environments may be enhanced

**Migration Actions**:

1. **Review Official Documentation**:
   - Check [.NET 10 breaking changes documentation](https://learn.microsoft.com/en-us/dotnet/core/compatibility/10.0) for UseExceptionHandler updates
   - Review ASP.NET Core 10.0 migration guide

2. **Test Error Handling Scenarios**:
   - Deliberately trigger exceptions in both applications
   - Verify error pages display correctly
   - Confirm exceptions are logged appropriately
   - Test both development and production configurations
   - Validate user experience during errors

3. **Code Review**:
   - Locate UseExceptionHandler calls (likely in Program.cs or Startup.cs)
   - Review any custom error handling configuration
   - Check for options or configuration that may need updating

4. **Potential Code Pattern**:
   ```csharp
   // Typical usage (may require adjustment based on .NET 10 changes)
   if (app.Environment.IsDevelopment())
   {
       app.UseDeveloperExceptionPage();
   }
   else
   {
       app.UseExceptionHandler("/Error");
       // Review if additional configuration is needed for .NET 10
   }
   ```

**Expected Impact**: Low
- No compilation errors expected
- Application will build and run
- Behavior changes only affect runtime exception handling
- Thorough testing will reveal any adjustments needed

### Framework-Level Breaking Changes

Beyond the specific API behavioral change, review general .NET 10 breaking changes:

**Areas to Review**:
1. **ASP.NET Core Changes**:
   - Middleware ordering requirements
   - Configuration system updates
   - Dependency injection changes
   - Endpoint routing modifications

2. **Runtime Changes**:
   - Globalization behavior
   - DateTime handling
   - String comparison defaults
   - Encoding defaults

3. **SDK Changes**:
   - Build behavior modifications
   - Default property values in project files
   - Implicit using directives

**Reference Documentation**:
- [Breaking changes in .NET 10](https://learn.microsoft.com/en-us/dotnet/core/compatibility/10.0)
- [ASP.NET Core 10.0 migration guide](https://learn.microsoft.com/en-us/aspnet/core/migration/90-to-100)
- [.NET 10 release notes](https://github.com/dotnet/core/tree/main/release-notes/10.0)

### No Breaking Changes Detected

✅ **Good news**: The following areas showed **no breaking changes**:

- **Binary Compatibility**: All existing compiled code remains compatible
- **Source Compatibility**: All source code compiles without modification
- **Package APIs**: The single package (Microsoft.AspNetCore.Http.Abstractions) should upgrade cleanly

### Validation Strategy

After upgrade, validate that no unexpected breaking changes were introduced:

1. **Compilation Validation**:
   - Full solution builds with 0 errors
   - Full solution builds with 0 warnings

2. **Runtime Validation**:
   - Both applications start without errors
   - Normal functionality works as expected
   - Error handling behaves correctly
   - No runtime exceptions occur during typical usage

3. **Behavioral Validation**:
   - Test exception scenarios in both apps
   - Verify error pages display correctly
   - Confirm logging output is appropriate
   - Compare behavior to .NET 7 version if needed

---

## Risk Management

### High-Level Assessment

**Overall Risk Level**: 🟢 **LOW**

This upgrade presents minimal risk due to:
- Small, well-structured codebase (600 LOC)
- Modern starting framework (.NET 7.0)
- No breaking API changes detected
- Only behavioral changes (low impact, testing only)
- Single package update required
- No security vulnerabilities
- All projects marked as Low difficulty

### Risk Breakdown by Category

| Risk Category | Level | Description | Mitigation |
|--------------|-------|-------------|------------|
| Framework Compatibility | 🟢 Low | .NET 7 to .NET 10 is forward-compatible path | Follow official migration guidance |
| Package Compatibility | 🟢 Low | Only 1 package requires update | Update to framework-compatible version |
| API Breaking Changes | 🟢 Low | No binary/source incompatibilities detected | N/A |
| Behavioral Changes | 🟡 Medium | UseExceptionHandler has behavioral changes (2 instances) | Thorough runtime testing of error handling |
| Build/Compilation | 🟢 Low | SDK-style projects, modern tooling | Standard build verification |
| Testing Coverage | 🟡 Medium | No test projects identified in solution | Manual testing required for both apps |

### Project-Specific Risks

| Project | Risk Level | Risk Factors | Mitigation Strategy |
|---------|-----------|--------------|---------------------|
| AspNetCore.RequestCounter | 🟢 Low | 1 outdated package, base library | Update package, verify dependent projects build |
| AspNetCore.RequestCounterTestBlazorApp | 🟡 Medium | 1 behavioral change (UseExceptionHandler), Blazor-specific testing needed | Test exception handling scenarios thoroughly |
| AspNetCore.RequestCounterTestWeb | 🟡 Medium | 1 behavioral change (UseExceptionHandler), Razor Pages-specific testing needed | Test exception handling scenarios thoroughly |

### Security Vulnerabilities

**None detected** - No packages with known security vulnerabilities.

### Contingency Plans

#### If Package Update Causes Issues

**Scenario**: Microsoft.AspNetCore.Http.Abstractions update introduces compatibility issues

**Mitigation**:
1. Review package release notes for .NET 10 compatibility
2. Check for alternative compatible versions
3. If no compatible version exists, investigate replacing with framework-provided abstractions (may be part of .NET 10 runtime)

#### If Behavioral Changes Impact Functionality

**Scenario**: UseExceptionHandler behavioral changes break error handling in test applications

**Mitigation**:
1. Review .NET 10 breaking changes documentation for UseExceptionHandler
2. Update error handling configuration to match new behavior
3. Adjust middleware pipeline if necessary
4. Update error pages/handlers to align with new behavior

#### If Build Fails After Framework Update

**Scenario**: Unexpected compilation errors after updating to net10.0

**Mitigation**:
1. Review compiler error messages for deprecated API usage
2. Consult .NET 10 migration guide for framework-specific changes
3. Update code to use replacement APIs
4. Verify all NuGet packages are compatible with .NET 10

### Rollback Strategy

If critical issues arise during upgrade:

1. **Immediate Rollback**: 
   - Discard changes on `upgrade-to-NET10` branch
   - Return to `master` branch
   - Solution remains on .NET 7.0

2. **Partial Rollback** (not applicable for All-at-Once strategy):
   - N/A - All-at-Once requires complete rollback or complete upgrade

3. **Post-Merge Rollback**:
   - Revert merge commit on master
   - Create new branch from pre-upgrade state
   - Re-attempt upgrade with adjusted approach

---

## Testing & Validation Strategy

### Overview

Since no automated test projects were identified in the solution, testing will rely on manual validation and runtime verification. This requires comprehensive testing of both applications after the upgrade.

### Multi-Level Testing Approach

#### Level 1: Compilation Validation (Immediate)

**Objective**: Ensure all projects build successfully after framework update

**Actions**:
1. Restore packages for entire solution
2. Build entire solution
3. Verify 0 compilation errors
4. Verify 0 compilation warnings

**Commands**:
```bash
dotnet restore AspNetCore.RequestCounter.sln
dotnet build AspNetCore.RequestCounter.sln --configuration Release
```

**Success Criteria**:
- All 3 projects restore successfully
- All 3 projects build without errors
- No warnings related to deprecated APIs
- No package compatibility warnings

---

#### Level 2: Project-Level Validation

**For Each Project**:

##### AspNetCore.RequestCounter (Library)

**Build Validation**:
- [ ] Project builds independently: `dotnet build AspNetCore.RequestCounter\AspNetCore.RequestCounter.csproj`
- [ ] No errors or warnings
- [ ] Package references resolve correctly

**Integration Validation**:
- [ ] Both dependent projects can reference the upgraded library
- [ ] No type resolution errors in consuming projects

##### AspNetCore.RequestCounterTestBlazorApp (Blazor Application)

**Build Validation**:
- [ ] Project builds independently
- [ ] Project reference to RequestCounter resolves
- [ ] All framework packages resolve to .NET 10 versions

**Runtime Validation**:
- [ ] Application starts: `dotnet run --project AspNetCore.RequestCounterTestBlazorApp`
- [ ] No startup errors or exceptions
- [ ] Application accessible in browser (default: https://localhost:5001 or http://localhost:5000)

**Functional Validation**:
- [ ] Home page loads correctly
- [ ] All Blazor components render
- [ ] Interactive components respond to user input
- [ ] Navigation between pages works
- [ ] No browser console errors
- [ ] Request counter functionality works (if applicable)

**Error Handling Validation** (Critical due to behavioral change):
- [ ] Trigger an exception deliberately (invalid route, null reference, etc.)
- [ ] Verify error page displays correctly
- [ ] Check that error details are appropriate for environment (detailed in dev, generic in production)
- [ ] Confirm application remains stable after error
- [ ] Verify exceptions are logged properly

##### AspNetCore.RequestCounterTestWeb (Razor Pages Application)

**Build Validation**:
- [ ] Project builds independently
- [ ] Both project references resolve correctly
- [ ] All framework packages resolve to .NET 10 versions

**Runtime Validation**:
- [ ] Application starts: `dotnet run --project AspNetCore.RequestCounterTestWeb`
- [ ] No startup errors or exceptions
- [ ] Application accessible in browser

**Functional Validation**:
- [ ] Home page loads correctly
- [ ] All Razor Pages render
- [ ] Page navigation works correctly
- [ ] Forms submit successfully (if applicable)
- [ ] Model binding works correctly
- [ ] Partial views and layouts render correctly
- [ ] Request counter functionality works (if applicable)

**Blazor Integration Validation**:
- [ ] Blazor app components render within Razor Pages context (if embedded)
- [ ] Navigation between Razor Pages and Blazor components works
- [ ] Shared services between Razor Pages and Blazor function correctly

**Error Handling Validation** (Critical due to behavioral change):
- [ ] Trigger an exception deliberately
- [ ] Verify Error.cshtml page displays correctly
- [ ] Check that error details are appropriate for environment
- [ ] Confirm application remains stable after error
- [ ] Verify exceptions are logged properly

---

#### Level 3: Comprehensive Solution Validation

**Full Solution Testing**:

**Build & Run**:
- [ ] Entire solution builds: `dotnet build AspNetCore.RequestCounter.sln`
- [ ] No errors across all projects
- [ ] No warnings across all projects
- [ ] Both applications can run simultaneously

**Integration Testing**:
- [ ] Test cross-project references work correctly
- [ ] Verify shared library (RequestCounter) functions in both apps
- [ ] Test any shared services or dependencies
- [ ] Verify dependency injection works across projects

**Performance Validation**:
- [ ] Application startup time is acceptable
- [ ] Page load times are comparable to .NET 7 version
- [ ] No obvious performance regressions
- [ ] Memory usage is reasonable

---

### Behavioral Change Testing (High Priority)

Due to UseExceptionHandler behavioral changes, dedicate specific testing effort:

**Test Scenarios for Both Applications**:

1. **Normal Exception Handling**:
   - Trigger a standard exception (e.g., divide by zero, null reference)
   - Verify error page appears
   - Confirm user-friendly message displayed

2. **HTTP Error Codes**:
   - Test 404 Not Found (invalid URL)
   - Test 500 Internal Server Error (forced exception)
   - Verify appropriate error pages for each

3. **Environment-Specific Behavior**:
   - Test in Development mode (detailed errors)
   - Test in Production mode (generic errors, no sensitive info)
   - Verify exception details are not leaked in Production

4. **Error Recovery**:
   - After error, navigate to valid page
   - Verify application continues functioning normally
   - Check for any lingering state issues

5. **Logging Verification**:
   - Trigger exception
   - Check application logs
   - Verify exception details are captured
   - Confirm log level is appropriate

---

### Regression Testing

**Compare .NET 10 behavior to .NET 7 baseline**:

If possible, run both versions side-by-side:
1. Keep .NET 7 version running on `master` branch
2. Run .NET 10 version on `upgrade-to-NET10` branch
3. Compare behavior across test scenarios
4. Document any differences observed
5. Assess whether differences are acceptable

---

### Smoke Testing Checklist

Quick validation after upgrade completion:

**AspNetCore.RequestCounterTestBlazorApp**:
- [ ] Application starts
- [ ] Home page loads
- [ ] At least one interactive component works
- [ ] Error handling tested

**AspNetCore.RequestCounterTestWeb**:
- [ ] Application starts
- [ ] Home page loads
- [ ] At least one Razor Page works
- [ ] Error handling tested

---

### Test Documentation

**Record Test Results**:

Create a test results document capturing:
- Date and time of testing
- .NET version confirmed (.NET 10.x.x)
- Test scenarios executed
- Results (Pass/Fail) for each scenario
- Any anomalies or unexpected behavior
- Performance observations
- Browser compatibility (Chrome, Firefox, Edge, etc.)

**Test Result Template**:
```markdown
## Upgrade Testing Results - .NET 10

**Date**: [Date]
**Tester**: [Name]
**.NET Version**: 10.x.x
**SDK Version**: [dotnet --version output]

### Build Results
- [ ] Solution builds: Pass/Fail
- [ ] Errors: [Count]
- [ ] Warnings: [Count]

### Blazor App Testing
- [ ] Startup: Pass/Fail
- [ ] Navigation: Pass/Fail
- [ ] Components: Pass/Fail
- [ ] Error Handling: Pass/Fail
- Notes: [Any observations]

### Razor Pages App Testing
- [ ] Startup: Pass/Fail
- [ ] Navigation: Pass/Fail
- [ ] Pages: Pass/Fail
- [ ] Error Handling: Pass/Fail
- Notes: [Any observations]

### Issues Found
[List any issues discovered]

### Overall Assessment
[Pass/Fail and summary]
```

---

### Success Criteria

Testing is complete and successful when:

✅ **Build Validation**:
- All projects build with 0 errors
- All projects build with 0 warnings
- All package references resolve correctly

✅ **Runtime Validation**:
- Both applications start without errors
- No unhandled exceptions during startup
- Applications are accessible in browser

✅ **Functional Validation**:
- All pages/components render correctly
- User interactions work as expected
- Navigation functions properly
- Request counter functionality works (if applicable)

✅ **Error Handling Validation** (Critical):
- Exception handler behaves correctly
- Error pages display appropriately
- Application remains stable after errors
- Logging captures exceptions properly

✅ **Performance Validation**:
- No significant performance degradation
- Startup and page load times acceptable

✅ **Compatibility Validation**:
- Works across common browsers
- No console errors or warnings

---

## Complexity & Effort Assessment

### Overall Complexity

**Classification**: 🟢 **LOW COMPLEXITY**

**Factors**:
- Small solution (3 projects, 600 LOC)
- Modern starting point (.NET 7.0)
- Simple dependency structure (linear chain)
- Minimal external dependencies (1 package)
- No security vulnerabilities
- No breaking API changes
- SDK-style projects (modern tooling)

### Per-Project Complexity

| Project | Complexity | Dependencies | Package Updates | API Issues | Risk | Justification |
|---------|-----------|--------------|-----------------|-----------|------|---------------|
| AspNetCore.RequestCounter | 🟢 Low | 0 | 1 | 0 | Low | Base library, single package update, no API issues |
| AspNetCore.RequestCounterTestBlazorApp | 🟢 Low | 1 | 0 | 1 | Low | Blazor app, 1 behavioral change, depends on base library |
| AspNetCore.RequestCounterTestWeb | 🟢 Low | 2 | 0 | 1 | Low | Razor Pages app, 1 behavioral change, depends on both projects |

### Phase Complexity Assessment

Since this is an All-at-Once strategy, there is a single phase:

**Phase 1: Atomic Upgrade**
- **Complexity**: 🟢 Low
- **Projects**: All 3 projects simultaneously
- **Dependencies**: Handled by build system automatically
- **Expected Changes**:
  - 3 TargetFramework updates (net7.0 → net10.0)
  - 1 package version update
  - 2 behavioral change validations
  - Full solution build verification

### Effort Breakdown by Activity

| Activity | Relative Complexity | Key Factors |
|----------|-------------------|-------------|
| Update Project Files | 🟢 Low | 3 simple TargetFramework changes, SDK-style projects |
| Update Packages | 🟢 Low | Single package update in 1 project |
| Build & Fix Compilation | 🟢 Low | No breaking changes expected, modern .NET migration |
| Address Behavioral Changes | 🟡 Medium | 2 instances of UseExceptionHandler behavior changes require testing |
| Testing & Validation | 🟡 Medium | No automated test projects, manual testing required |
| Documentation | 🟢 Low | Small codebase, minimal changes to document |

### Resource Requirements

**Skill Level Required**: 🟢 Intermediate
- Familiarity with .NET project structure
- Understanding of ASP.NET Core middleware
- Ability to test Blazor and Razor Pages applications
- Basic knowledge of NuGet package management

**Team Capacity**: Single developer sufficient
- All changes can be completed by one person
- No parallel work required (All-at-Once atomic operation)
- Minimal coordination overhead

**Tools Required**:
- .NET 10 SDK installed
- Visual Studio 2022 or later / VS Code with C# extension
- Git for version control

### Complexity Comparison

**Relative to typical .NET upgrades**:
- **Much simpler** than .NET Framework → .NET Core migrations
- **Simpler** than multi-version jumps (e.g., .NET 6 → .NET 10)
- **Comparable** to single-version incremental upgrades (.NET 8 → .NET 9)
- **Lower risk** due to small codebase and modern starting point

---

## Source Control Strategy

### Branching Strategy

**Current Branch**: `upgrade-to-NET10` (created specifically for this upgrade)
**Source Branch**: `master`
**Merge Target**: `master` (after successful upgrade and testing)

### Branch Workflow

```
master (net7.0)
   └─> upgrade-to-NET10 (net10.0) ──> merge back to master
```

**Branch Purpose**:
- `upgrade-to-NET10` isolates all upgrade changes
- Allows easy rollback if issues arise
- Permits continued development on `master` if needed
- Provides clear upgrade history

---

### Commit Strategy

For All-at-Once strategy, use **single comprehensive commit** approach:

#### Recommended: Single Atomic Commit

**Rationale**:
- All-at-Once strategy performs atomic upgrade
- Single commit reflects single logical change
- Easier to revert if needed
- Clearer history (one commit = one upgrade)
- Matches the atomic nature of the strategy

**Commit Message Template**:
```
Upgrade solution from .NET 7 to .NET 10

- Update all 3 project files to target net10.0
- Update Microsoft.AspNetCore.Http.Abstractions package in RequestCounter
- Validate UseExceptionHandler behavioral changes in both test apps
- All projects build successfully with 0 errors/warnings
- Both applications tested and functioning correctly

Projects upgraded:
- AspNetCore.RequestCounter (net7.0 → net10.0)
- AspNetCore.RequestCounterTestBlazorApp (net7.0 → net10.0)
- AspNetCore.RequestCounterTestWeb (net7.0 → net10.0)

Breaking changes addressed:
- UseExceptionHandler behavioral change tested and validated

Testing completed:
- Compilation validation: Pass
- Blazor app functional testing: Pass
- Razor Pages app functional testing: Pass
- Error handling validation: Pass
```

#### Alternative: Multi-Commit Approach

If you prefer more granular history:

**Commit 1**: Framework updates
```
Update all projects to .NET 10

- Update TargetFramework in all 3 project files
- AspNetCore.RequestCounter: net7.0 → net10.0
- AspNetCore.RequestCounterTestBlazorApp: net7.0 → net10.0
- AspNetCore.RequestCounterTestWeb: net7.0 → net10.0
```

**Commit 2**: Package updates
```
Update NuGet packages for .NET 10 compatibility

- Update/remove Microsoft.AspNetCore.Http.Abstractions in RequestCounter
- Restore all packages for solution
```

**Commit 3**: Behavioral changes validation
```
Validate UseExceptionHandler behavioral changes

- Test exception handling in Blazor app
- Test exception handling in Razor Pages app
- Confirm error pages function correctly
- No code changes required
```

**Commit 4**: Final validation
```
Complete .NET 10 upgrade validation

- All projects build with 0 errors
- All projects build with 0 warnings
- Both applications tested and functioning
- Upgrade complete and ready for merge
```

---

### Commit Best Practices

**Commit When**:
- After each logical group of changes completes
- Before testing major changes
- After fixing compilation errors
- After successful validation

**Commit Message Guidelines**:
- Use clear, descriptive messages
- Reference the upgrade in each message
- Include specific version numbers
- List affected projects
- Note any breaking changes addressed
- Include testing outcomes

**What to Commit**:
- ✅ Project file changes (.csproj)
- ✅ Code modifications (if any)
- ✅ Configuration updates (if any)
- ✅ Documentation updates
- ❌ Do NOT commit: bin/, obj/, .vs/, user-specific files

---

### Review and Merge Process

#### Pre-Merge Checklist

Before merging `upgrade-to-NET10` → `master`:

**Technical Validation**:
- [ ] All projects build with 0 errors
- [ ] All projects build with 0 warnings
- [ ] Both applications run successfully
- [ ] All functional tests pass
- [ ] Error handling validated
- [ ] No package vulnerabilities
- [ ] No performance regressions

**Code Review**:
- [ ] Review all project file changes
- [ ] Review any code modifications
- [ ] Verify package updates are correct
- [ ] Confirm commit messages are clear
- [ ] Check for any temporary/debug code

**Documentation**:
- [ ] Update README if needed (new .NET version requirement)
- [ ] Update build instructions if needed
- [ ] Document any behavioral changes discovered
- [ ] Note any post-upgrade considerations

#### Merge Strategy

**Recommended: Merge Commit**

```bash
git checkout master
git merge --no-ff upgrade-to-NET10 -m "Merge .NET 10 upgrade"
```

**Rationale**:
- Preserves upgrade branch history
- Clear merge commit in history
- Easy to identify upgrade in git log
- Maintains all commit details

**Merge Commit Message Template**:
```
Merge .NET 10 upgrade

Merges upgrade-to-NET10 branch containing complete solution upgrade from .NET 7 to .NET 10.

All projects successfully upgraded and tested:
- AspNetCore.RequestCounter
- AspNetCore.RequestCounterTestBlazorApp
- AspNetCore.RequestCounterTestWeb

Validation complete:
- Builds: Pass
- Functional tests: Pass
- Error handling: Pass
- Performance: Acceptable

All success criteria met.
```

**Alternative: Squash Merge** (if multi-commit approach was used and you want cleaner history)

```bash
git checkout master
git merge --squash upgrade-to-NET10
git commit -m "Upgrade solution from .NET 7 to .NET 10"
```

#### Post-Merge Actions

After merging to `master`:

1. **Tag the Release**:
   ```bash
   git tag -a net10-upgrade -m ".NET 10 upgrade completed"
   git push origin net10-upgrade
   ```

2. **Delete Feature Branch** (optional):
   ```bash
   git branch -d upgrade-to-NET10
   git push origin --delete upgrade-to-NET10
   ```

3. **Update CI/CD** (if applicable):
   - Update build pipelines to use .NET 10 SDK
   - Update deployment configurations
   - Update Docker images to .NET 10 runtime

4. **Notify Team**:
   - Announce upgrade completion
   - Note any behavioral changes
   - Provide upgrade documentation
   - Share testing results

---

### Rollback Strategy

If critical issues are discovered after merge:

**Immediate Rollback**:
```bash
git revert -m 1 <merge-commit-hash>
git push origin master
```

**Alternative: Reset to Pre-Upgrade State** (if merge just happened and no other commits exist):
```bash
git reset --hard HEAD~1
git push origin master --force
```

**Best Practice**: Only force push if absolutely necessary and team is coordinated.

---

### Branch Cleanup

**Keep `upgrade-to-NET10` branch if**:
- Issues discovered post-merge
- Need to reference upgrade process
- Want to preserve detailed history

**Delete `upgrade-to-NET10` branch if**:
- Upgrade successful and stable
- History preserved in merge commit
- No need for separate branch reference

---

### Git Workflow Summary

**Single Commit Approach** (Recommended for All-at-Once):
1. Make all changes on `upgrade-to-NET10` branch
2. Test thoroughly
3. Create single comprehensive commit
4. Merge to `master` with merge commit
5. Tag the release
6. Optionally delete feature branch

**Multi-Commit Approach**:
1. Make changes incrementally on `upgrade-to-NET10`
2. Commit after each logical step
3. Test thoroughly
4. Merge to `master` (merge commit or squash)
5. Tag the release
6. Optionally delete feature branch

**Both approaches are valid** - choose based on team preference and history granularity needs.

---

## Success Criteria

### Technical Criteria

The upgrade is technically successful when all of the following are achieved:

#### ✅ Framework Migration Complete

- [ ] **All projects migrated**: All 3 projects target net10.0
  - AspNetCore.RequestCounter: net7.0 → net10.0 ✓
  - AspNetCore.RequestCounterTestBlazorApp: net7.0 → net10.0 ✓
  - AspNetCore.RequestCounterTestWeb: net7.0 → net10.0 ✓

- [ ] **Project files updated**: All `<TargetFramework>` elements updated to `net10.0`

- [ ] **.NET 10 SDK verified**: Solution builds using .NET 10 SDK

#### ✅ Package Updates Complete

- [ ] **All package updates applied**: Microsoft.AspNetCore.Http.Abstractions updated or removed

- [ ] **Framework packages resolved**: All implicit framework packages resolve to .NET 10 versions

- [ ] **No package conflicts**: `dotnet restore` completes without conflicts

- [ ] **No vulnerabilities**: `dotnet list package --vulnerable` shows no vulnerable packages

- [ ] **No deprecated packages**: All packages are current and supported

#### ✅ Build Success

- [ ] **Solution builds**: `dotnet build AspNetCore.RequestCounter.sln` succeeds

- [ ] **Zero compilation errors**: No errors in any project

- [ ] **Zero warnings**: No warnings in any project

- [ ] **All projects build individually**: Each project builds independently

- [ ] **Release configuration builds**: `dotnet build --configuration Release` succeeds

#### ✅ Runtime Success

- [ ] **Blazor app starts**: AspNetCore.RequestCounterTestBlazorApp runs without errors

- [ ] **Razor Pages app starts**: AspNetCore.RequestCounterTestWeb runs without errors

- [ ] **No startup exceptions**: Both applications complete startup successfully

- [ ] **Applications accessible**: Both applications accessible via browser

#### ✅ Functional Validation

- [ ] **Blazor app functional**: All components render and interactive features work

- [ ] **Razor Pages app functional**: All pages render and navigation works

- [ ] **Request counter works**: Core library functionality verified in both apps

- [ ] **Project references work**: Dependent projects correctly reference upgraded libraries

- [ ] **Shared functionality works**: Any shared services or components function correctly

#### ✅ Error Handling Validation (Critical)

- [ ] **Exception handler tested in Blazor app**: UseExceptionHandler behavioral change validated

- [ ] **Exception handler tested in Razor Pages app**: UseExceptionHandler behavioral change validated

- [ ] **Error pages display correctly**: Custom error pages render as expected

- [ ] **Exceptions logged properly**: Exception logging functions correctly

- [ ] **Application stable after errors**: Both apps recover gracefully from exceptions

---

### Quality Criteria

The upgrade meets quality standards when:

#### Code Quality Maintained

- [ ] **No code degradation**: Code quality equal to or better than .NET 7 version

- [ ] **No technical debt introduced**: No shortcuts or workarounds added

- [ ] **Clean code**: No commented-out code, debug statements, or temporary fixes

- [ ] **Consistent patterns**: All projects follow consistent upgrade patterns

#### Performance Maintained

- [ ] **No performance regression**: Application performance comparable to .NET 7

- [ ] **Startup time acceptable**: No significant increase in startup time

- [ ] **Response time acceptable**: Page load times within acceptable range

- [ ] **Memory usage reasonable**: No excessive memory consumption

#### Documentation Updated

- [ ] **README updated**: Build instructions reflect .NET 10 requirement

- [ ] **Dependencies documented**: Any new dependencies or requirements noted

- [ ] **Breaking changes documented**: Behavioral changes and mitigations documented

- [ ] **Upgrade process documented**: Migration steps captured for future reference

---

### Process Criteria

The upgrade follows proper process when:

#### All-at-Once Strategy Executed

- [ ] **Atomic upgrade**: All projects upgraded simultaneously in single operation

- [ ] **No intermediate states**: No mixed framework versions during upgrade

- [ ] **Strategy principles followed**: All-at-Once guidelines adhered to

- [ ] **Dependency order respected**: Logical dependency order maintained (informational)

#### Source Control Followed

- [ ] **Upgrade branch used**: All changes committed to `upgrade-to-NET10` branch

- [ ] **Clear commit history**: Commits have descriptive messages

- [ ] **Single commit approach**: Preferably one comprehensive commit (recommended for All-at-Once)

- [ ] **No uncommitted changes**: All changes committed before merge

#### Testing Completed

- [ ] **Compilation testing**: All build validation steps completed

- [ ] **Runtime testing**: Both applications tested manually

- [ ] **Error handling testing**: Exception scenarios tested thoroughly

- [ ] **Smoke testing**: Quick validation checklist completed

- [ ] **Test results documented**: Testing outcomes recorded

#### Review Completed

- [ ] **Code review**: All changes reviewed

- [ ] **Project file review**: All .csproj changes verified correct

- [ ] **Package update review**: Package updates verified appropriate

- [ ] **Documentation review**: Updated documentation reviewed

---

### Acceptance Criteria

The upgrade is ready for merge to `master` when:

#### All Technical Criteria Met

✅ All technical criteria checkboxes above are checked

#### All Quality Criteria Met

✅ All quality criteria checkboxes above are checked

#### All Process Criteria Met

✅ All process criteria checkboxes above are checked

#### Stakeholder Approval

- [ ] **Team approval**: Development team approves upgrade

- [ ] **Testing sign-off**: Testing results reviewed and approved

- [ ] **Documentation approval**: Updated documentation approved

---

### Definition of Done

**The .NET 10 upgrade is DONE when**:

1. ✅ All 3 projects successfully target .NET 10
2. ✅ All package updates applied correctly
3. ✅ Entire solution builds with 0 errors and 0 warnings
4. ✅ Both test applications run successfully
5. ✅ UseExceptionHandler behavioral changes validated in both apps
6. ✅ All functional testing completed successfully
7. ✅ No performance regressions detected
8. ✅ No security vulnerabilities present
9. ✅ Documentation updated to reflect .NET 10
10. ✅ All changes committed with clear messages
11. ✅ Code review completed
12. ✅ Testing results documented
13. ✅ Upgrade branch ready for merge to `master`

**At this point**, the upgrade is complete and the `upgrade-to-NET10` branch can be merged to `master`.

---

### Post-Upgrade Verification

After merging to `master`, verify:

- [ ] **Master branch builds**: Solution builds successfully on `master`

- [ ] **CI/CD pipeline passes**: Automated builds succeed (if applicable)

- [ ] **Deployment successful**: Applications deploy to target environments (if applicable)

- [ ] **Production validation**: Applications function in production environment (if applicable)

- [ ] **Team updated**: All developers aware of .NET 10 requirement

- [ ] **Tooling updated**: CI/CD, Docker, and other tooling updated to .NET 10

---

### Success Metrics Summary

| Category | Criteria | Target |
|----------|----------|--------|
| **Framework** | Projects on .NET 10 | 3 / 3 (100%) |
| **Build** | Compilation errors | 0 |
| **Build** | Compilation warnings | 0 |
| **Packages** | Updates applied | 1 / 1 (100%) |
| **Packages** | Vulnerabilities | 0 |
| **Runtime** | Applications starting | 2 / 2 (100%) |
| **Functional** | Features working | All critical features |
| **Breaking Changes** | Behavioral changes validated | 2 / 2 (100%) |
| **Performance** | Regression | None acceptable |
| **Quality** | Code quality | Maintained or improved |

**Overall Success**: All metrics meet or exceed targets
