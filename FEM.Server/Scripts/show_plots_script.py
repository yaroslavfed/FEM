import json
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
from matplotlib.cm import ScalarMappable
from matplotlib.colors import Normalize
from matplotlib.patches import Polygon
from dataclasses import dataclass
from typing import List

@dataclass
class Point3D:
    X: float
    Y: float
    Z: float

@dataclass
class Node:
    NodeIndex: int
    Coordinate: Point3D

@dataclass
class Edge:
    EdgeIndex: int
    Nodes: List[Node]

@dataclass
class Face:
    Nodes: List[Node]

@dataclass
class FiniteElement:
    Density: float
    Edges: List[Edge]
    Faces: List[Face]

@dataclass
class MeshData:
    Elements: List[FiniteElement]

def load_from_json(file_path: str) -> MeshData:
    with open(file_path, 'r') as f:
        data = json.load(f)

    elements = []
    for element in data['Elements']:
        faces = [
            Face(Nodes=[
                Node(
                    NodeIndex=n['NodeIndex'],
                    Coordinate=Point3D(**n['Coordinate'])
                ) for n in face['Nodes']
            ]) for face in element.get('Faces', [])
        ]

        elements.append(FiniteElement(
            Density=element['Density'],
            Edges=[
                Edge(
                    EdgeIndex=edge['EdgeIndex'],
                    Nodes=[
                        Node(
                            NodeIndex=n['NodeIndex'],
                            Coordinate=Point3D(**n['Coordinate'])
                        ) for n in edge['Nodes']
                    ]
                ) for edge in element['Edges']
            ],
            Faces=faces
        ))

    return MeshData(Elements=elements)

def plot_finite_element_mesh(elements: List[FiniteElement]):
    fig = plt.figure(figsize=(18, 12))

    # Настройка сетки графиков
    gs = fig.add_gridspec(
        nrows=2,
        ncols=2,
        width_ratios=[1, 1],
        height_ratios=[1, 1],
        left=0.05,
        right=0.88,
        top=0.95,
        bottom=0.05,
        wspace=0.3,
        hspace=0.3
    )

    # Инициализация осей
    ax3d = fig.add_subplot(gs[0, 0], projection='3d')
    ax_xy = fig.add_subplot(gs[0, 1])
    ax_xz = fig.add_subplot(gs[1, 0])
    ax_yz = fig.add_subplot(gs[1, 1])

    # Настройка цветовой карты
    densities = [element.Density for element in elements]
    norm = Normalize(vmin=min(densities), vmax=max(densities))
    cmap = plt.get_cmap('viridis')
    mappable = ScalarMappable(norm=norm, cmap=cmap)

    # Отрисовка 3D ребер
    for element in elements:
        color = cmap(norm(element.Density))
        for edge in element.Edges:
            if len(edge.Nodes) != 2:
                continue
            n1, n2 = edge.Nodes[0], edge.Nodes[1]
            ax3d.plot(
                [n1.Coordinate.X, n2.Coordinate.X],
                [n1.Coordinate.Y, n2.Coordinate.Y],
                [n1.Coordinate.Z, n2.Coordinate.Z],
                color=color, linewidth=1.5, alpha=0.7
            )

    # Отрисовка 2D проекций с заливкой
    for ax, proj_plane in zip([ax_xy, ax_xz, ax_yz], ['xy', 'xz', 'yz']):
        for element in elements:
            color = cmap(norm(element.Density))
            for face in element.Faces:
                coords = []
                for node in face.Nodes:
                    if proj_plane == 'xy':
                        x, y = node.Coordinate.X, node.Coordinate.Y
                    elif proj_plane == 'xz':
                        x, y = node.Coordinate.X, node.Coordinate.Z
                    else:
                        x, y = node.Coordinate.Y, node.Coordinate.Z
                    coords.append((x, y))

                poly = Polygon(
                    coords,
                    closed=True,
                    facecolor=color,
                    edgecolor='k',
                    alpha=0.5,
                    linewidth=0.5
                )
                ax.add_patch(poly)

        ax.autoscale_view()
        ax.set_aspect('equal')
        ax.set_title(f'{proj_plane.upper()} Projection')
        ax.set_xlabel('X' if proj_plane in ['xy', 'xz'] else 'Y')
        ax.set_ylabel('Y' if proj_plane == 'xy' else 'Z')

    # Настройка 3D вида
    ax3d.set_xlabel('X')
    ax3d.set_ylabel('Y')
    ax3d.set_zlabel('Z')
    ax3d.set_title('3D View')

    # Цветовая шкала
    cbar_ax = fig.add_axes([0.90, 0.15, 0.02, 0.7])
    fig.colorbar(mappable, cax=cbar_ax, label='Density (kg/m³)')

    plt.show()

if __name__ == "__main__":
    # Пример использования
    try:
        mesh_data = load_from_json('mesh_data.json')
        plot_finite_element_mesh(mesh_data.Elements)
    except Exception as e:
        print(f"Error: {str(e)}")