enum Branches {
    working
    jacob
}

enum BranchType {
    Known
    Empty
    Unknown
}

function StringToBranches([string] $string) {
	if ($string -eq "") {
		return [BranchType]::Empty
	} elseif (StringIsKnownBranch($string)) {
        return [BranchType]::Known
	} else {
		return [BranchType]::Unknown
	}
}

function StringIsKnownBranch([string] $string) {
    [bool]$isKnownBranch = $false
    [Branches].GetEnumNames() | ForEach-Object {
        if ($_ -eq $string)
        {
            $isKnownBranch = $true
        }
    }
    return $isKnownBranch
}

function BranchToString([Branches] $branch) {
    Switch ($branch) {
        ([Branches]::working) {
            return "NewWorkingBranch"
        }
        ([Branches]::jacob) {
            return "JacobBranch1"
        }
    }
}

function Fetch() {
	param(
		[string]$Branch
	)
	Switch(StringToBranches($Branch)) {
        ([BranchType]::Known) {
            git fetch origin $(BranchToString($Branch))
        }
        ([BranchType]::Empty) {
			Write-Output "Please provide a branch"
			break
		}
        ([BranchType]::Unknown) {
			git fetch origin ${branch}:${branch}
			break
		}
	}
}

function Rebase() {
	param(
		[string]$Branch
	)
	Switch(StringToBranches($Branch)) {
        ([BranchType]::Known) {
			git rebase $(BranchToString($Branch))
		}
        ([BranchType]::Empty) {
			Write-Output "Please provide a branch"
		}
        ([BranchType]::Unknown) {
			git rebase ${branch}
		}
	}
}

function Checkout() {
	param(
		[string]$Branch
	)
	Switch(StringToBranches($Branch)) {
        ([BranchType]::Known) {
			git checkout $(BranchToString($Branch))
		}
        ([BranchType]::Empty) {
			Write-Output "Please provide a branch"
		}
        ([BranchType]::Unknown) {
			git checkout ${branch}
		}

	}
}

function DeleteBranch() {
	param(
		[string]$Branch
	)
	git branch -d ${Branch}
}

function Push() {
	param(
		[string]$Branch,
		[string]$Remote,
		[switch]$Upstream
	)
	if (!$Remote) {
		$Remote = "origin"
	}
	git push $(if ($Upstream) { "-u" }) $Remote $(if ($Branch) {$Branch})
}

function Pull() {
	param(
		[string]$Branch,
		[string]$Remote
	)
	if (!$Remote) {
		$Remote = "origin"
	}
	git pull $Remote $(if ($Branch) {$Branch})
}

function Branch() {
	param(
		[switch]$Delete,
		[string]$Branch
	)
	git branch $(if ($Delete) { return "-d" })
}

Register-ArgumentCompleter -CommandName Fetch -ParameterName Branch -ScriptBlock {
    [Branches].GetEnumNames()
}

Register-ArgumentCompleter -CommandName Rebase -ParameterName Branch -ScriptBlock {
    [Branches].GetEnumNames()
}

Register-ArgumentCompleter -CommandName Checkout -ParameterName Branch -ScriptBlock {
    [Branches].GetEnumNames()
}

Register-ArgumentCompleter -CommandName Branch -ParameterName Branch -ScriptBlock {
	git for-each-ref --format='%(refname:short)' refs/heads/
}
