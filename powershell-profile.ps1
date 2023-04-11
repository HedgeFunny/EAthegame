function Fetch($branch) {
	Switch($branch) {
		"working" {
			git fetch origin WorkingBranch:WorkingBranch
		}
		"jacob" {
			git fetch origin JacobBranch:JacobBranch
		}
		"" {
			Write-Output "Please provide a branch"
		}
		Default {
			git fetch origin ${branch}:${branch}
		}
	}
}

function Rebase($branch) {
	Switch($branch) {
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

function Checkout($branch) {
	Switch($branch) {
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

function DeleteBranch($branch) {
	git branch -d ${branch}
}

function Push() {
	param(
		[string]$Branch,
		[string]$Remote
	)
	if (!$Branch) {
		$Branch = "";
	}
	if (!$Remote) {
		$Remote = "origin"
	}
	git push $Remote $Branch
}