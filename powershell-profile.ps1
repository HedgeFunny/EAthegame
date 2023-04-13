function Fetch() {
	param(
		[string]$Branch
	)
	Switch($Branch) {
		"working" {
			git fetch origin WorkingBranch:WorkingBranch
			break
		}
		"jacob" {
			git fetch origin JacobBranch:JacobBranch
			break
		}
		"" {
			Write-Output "Please provide a branch"
			break
		}
		Default {
			git fetch origin ${branch}:${branch}
			break
		}
	}
}

function Rebase() {
	param(
		[string]$Branch
	)
	Switch($Branch) {
		"working" {
			git rebase WorkingBranch
		}
		"" {
			Write-Output "Please provide a branch"
		}
		Default {
			git rebase ${branch}
		}
	}
}

function Checkout() {
	param(
		[string]$Branch
	)
	Switch($Branch) {
		"working" {
			git checkout WorkingBranch
		}
		"jacob" {
			git checkout JacobBranch
		}
		"" {
			Write-Output "Please provide a branch"
		}
		Default {
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
		[string]$Remote
	)
	if (!$Branch) {
		$Branch = ""
	}
	if (!$Remote) {
		$Remote = "origin"
	}
	git push $Remote $Branch
}

function Pull() {
	param(
		[string]$Branch,
		[string]$Remote
	)
	if (!$Branch) {
		$Branch = ""
	}
	if (!$Remote) {
		$Remote = "origin"
	}
	git pull $Remote $Branch
}

Register-ArgumentCompleter -CommandName Fetch -ParameterName Branch -ScriptBlock {
	"working","jacob"
}

Register-ArgumentCompleter -CommandName Rebase -ParameterName Branch -ScriptBlock {
	"working"
}

Register-ArgumentCompleter -CommandName Checkout -ParameterName Branch -ScriptBlock {
	"working","jacob"
}
