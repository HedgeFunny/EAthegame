function Fetch() {
	param(
		[string]$Branch
	)
	Switch($Branch) {
		"working" {
			git fetch origin WorkingBranch3:WorkingBranch3
			break
		}
		"jacob" {
			git fetch origin JacobBranch1:JacobBranch1
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
			git rebase WorkingBranch3
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
			git checkout WorkingBranch3
		}
		"jacob" {
			git checkout JacobBranch1
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
	if (!$Remote) {
		$Remote = "origin"
	}
	git push $Remote $(if ($Branch) {$Branch})
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

Register-ArgumentCompleter -CommandName Fetch -ParameterName Branch -ScriptBlock {
	"working","jacob"
}

Register-ArgumentCompleter -CommandName Rebase -ParameterName Branch -ScriptBlock {
	"working"
}

Register-ArgumentCompleter -CommandName Checkout -ParameterName Branch -ScriptBlock {
	"working","jacob"
}
