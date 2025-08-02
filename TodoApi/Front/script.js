const API_BASE_URL = 'https://localhost:7286/api';


let todos = [];

const formNovaTarefa = document.querySelector('#form-nova-tarefa');
const inputNovaTarefa = document.querySelector('#input-nova-tarefa');
const listaTarefas = document.querySelector('#lista-tarefas');


console.log("Vericando o formulário:", formNovaTarefa);
console.log("Vericando o input", inputNovaTarefa);
console.log("Vericando a lista:", listaTarefas);


formNovaTarefa.addEventListener('submit', async (evento) => {

    evento.preventDefault();

    const textoDaTarefa = inputNovaTarefa.value;

    console.log('Texto digitado', textoDaTarefa);

    if (textoDaTarefa.trim() === '') {
        alert('Por favor, digite uma tarefa.');
        return;
    }
    await addTodoAPI(textoDaTarefa);

    inputNovaTarefa.value = '';

    fetchTodos();
});


async function fetchTodos() {
    console.log('Buscando tarefas na API...');
    try {
        const response = await fetch(`${API_BASE_URL}/todos`);

        if (!response.ok) {

            throw new Error(`Erro na API: ${Response.status}`);
        }
        const tasksFromApi = await response.json();

        todos = tasksFromApi;

        renderTodos();
    } catch (error) {
        console.error('Erro ao buscar tarefas:', error);
        alert('Não foi possivel carregar as tarefas. Verifique se o backend está rodando.');
    }
}

async function addTodoAPI(textoDaTarefa) {
    console.log('Enviando nova tarefa para a API...');


    const novaTarefa = {
        text: textoDaTarefa  
    };
    try {
        const response = await fetch(`${API_BASE_URL}/todos`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(novaTarefa)
        });
        if (!response.ok) {
            throw new Error(`Erro ao adicionar tarefa: ${response.status}`);
        }
        return true;
    } catch (error) {
        console.error('Falha na funçãao addTodoAPI', error);
        alert('Não foi possivel adicionar a tarefa.');
        return false;
    }
   
}

function renderTodos() {
    console.log('Renderizando lista de tarefas...');

    listaTarefas.innerHTML = '';

    if (todos.length === 0) {
        const itemVazio = document.createElement('li');
        itemVazio.textContext = 'Nenhuma tarefa encontrada. Adicione uma nova!';
        itemVazio.classList.add('Empty');
        listaTarefas.appendChild(itemVazio);
        return;
    }

    todos.forEach(task => {

        const li = document.createElement('li');

        li.dataset.id = task.id;

        if (task.isCompleted) {
            li.classList.add('completed');
        }

        li.innerHTML = `
        <span class="task-text">${task.text}</span>
        <button class="delete-btn">X</button>
        `;

        listaTarefas.appendChild(li);
    });
}

async function deleteTodoAPI(id) {

    console.log('Enviando a requisição para apagar a Tarefa ${id^}...');

    try {
        const response = await fetch('${API_BASE_URL}/todos/${id}', {
            method: 'DELETE'
        });

        if (!response.ok) {
            throw new Error('Erro na API ao apagar: ${response.status}');
        }

        return true;
    } catch (error) {
        console.error('Falha ao apagar tarefa:', error);
        alert('Não foi possivel apagar a tarefa.');
        return false;
    }
}


listaTarefas.addEventListener('click', async (evento) => {
    if (evento.target.classList.contains('delete-btn')) {

        const itemDaLista = evento.target.closest('li');

        const idDaTarefa = itemDaLista.dataset.id;

        console.log('Tentando apagar a tarefa com ID:', idDaTarefa);

        const sucesso = await deleteTodoAPI(idDaTarefa);

        if (sucesso) {
            fetchTodos();
        }

    }

});


document.addEventListener('DOMContentLoaded', () => {
    console.log('Página carregada. Aplicação Iniciada.')

    fetchTodos();
});

